using System.Text.Json.Serialization;
using CityLibrary.ServicesExtensions;
using CityLibraryApi.MapperConfigurations;
using CityLibraryInfrastructure.AppSettings;
using CityLibraryInfrastructure.ExceptionHandling.Extensions;
using CityLibraryInfrastructure.Extensions;
using CityLibraryInfrastructure.Extensions.TokenExtensions;
using CityLibraryInfrastructure.Resources;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});

var configuration = builder.Configuration;
// IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<AppSetting>(configuration);
var appSetting = configuration.Get<AppSetting>();

builder.Services.AddCustomLocalizationConfiguration();

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Continue;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<MapsterMapping>();

builder.Services.AddRangeCustomServices(appSetting);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddCustomSwaggerConfiguration();

builder.Services.AddCors();

var app = builder.Build();

GenerateAndVerifyPasswords.Localizer =
    app.Services.GetService<IStringLocalizer<ExceptionsResource>>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CityLibrary v1"));
}

app.UseSerilogRequestLogging();

app.UseRequestLocalization();

app.UseStaticHttpContext();

app.UseCors(opts =>
        // Content dispositon is useful for getting file name for frontend application
        opts.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")
);

app.UseCustomGlobalExceptionHandler(app.Services.GetService<IStringLocalizer<SharedResource>>());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

