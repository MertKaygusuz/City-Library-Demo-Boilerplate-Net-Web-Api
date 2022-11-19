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

var builder = WebApplication.CreateBuilder(args);

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CityLibrary v1"));
}

app.UseRequestLocalization();

app.UseStaticHttpContext();

app.UseCors(opts =>
        //TODO: content disposition explanation
        opts.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition")
);

app.UseCustomGlobalExceptionHandler();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

