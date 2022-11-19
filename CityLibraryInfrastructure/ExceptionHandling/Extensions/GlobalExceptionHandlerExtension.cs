using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using CityLibraryInfrastructure.ExceptionHandling.Dtos;
using Microsoft.Extensions.Hosting;

namespace CityLibraryInfrastructure.ExceptionHandling.Extensions;

public static class GlobalExceptionHandlerExtension
{
    public static void UseCustomGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature!.Error;

                if (exception is CustomHttpException exception1)
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    context.Response.StatusCode = (int)exception1.HttpStatusCode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDto(exception.Message, (int)exception1.HttpStatusCode)));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var errorObject = new ErrorDto("Internal Server Error", 500);
                    if (app.Environment.IsDevelopment())
                    {
                        errorObject.InternalErrorMessage = exception.Message;
                        errorObject.InternalSource = exception.Source;
                        errorObject.InternalStackTrace = exception.StackTrace;
                    }
                        
                    // ExceptionDispatchInfo.Capture(exception).Throw();
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorObject));
                }
            });
        });
    }
}