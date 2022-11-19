using CityLibrary.ActionFilters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityLibraryInfrastructure.ExceptionHandling.Dtos;

namespace CityLibrary.ActionFilters.Classes
{
    public class RefreshLoginFilter : IRefreshLoginFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string refreshToken = context.ActionArguments["refreshToken"] as string;

            if (string.IsNullOrEmpty(refreshToken))
            {
                var err = new ErrorDto("Refresh token is empty.");
                context.Result = new ObjectResult(err)
                {
                    StatusCode = err.Status
                };
                return;
            }
            
            await next();
        }
    }
}
