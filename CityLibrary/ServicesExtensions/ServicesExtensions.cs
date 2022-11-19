using CityLibrary.ActionFilters.Base;
using CityLibraryApi.MapperConfigurations;
using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.AppSettings;
using CityLibraryInfrastructure.DbBase;
using CityLibraryInfrastructure.MapperConfigurations;
using static CityLibraryInfrastructure.Statics.Methods.TokenRelated;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;

namespace CityLibrary.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddRangeCustomServices(this IServiceCollection services, AppSetting appSetting)
        {
            services.AddCustomJwtService(appSetting.TokenOptions);
            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(options =>
            {
                var triggerAssembly = Assembly.GetAssembly(typeof(AppDbContext));
                options.UseTriggers(triggerOptions => triggerOptions.AddAssemblyTriggers(triggerAssembly!));
                options.UseInMemoryDatabase(appSetting.DbConnectionString);
            });

            services.AddSingleton<ICustomMapper, MapsterMapping>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSetting.RedisConnection.ConnectionString;
                options.InstanceName = appSetting.RedisConnection.InstanceName;
            });

            services.AddScoped(typeof(IBaseRepo<TableBase, IConvertible>), typeof(BaseRepo<TableBase, IConvertible>));

            var assembliesToScan = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(AppSetting)),
                Assembly.GetAssembly(typeof(AppDbContext)),
                Assembly.GetAssembly(typeof(MapsterMapping))
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
              .Where(c => c.Name.EndsWith("Repo"))
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
              .Where(c => c.Name.EndsWith("Service"))
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);

            services.AddTransientServices(assembliesToScan);
        }

        private static void AddTransientServices(this IServiceCollection services, Assembly[] assembliesToScan)
        {
            services.AddTransient(typeof(GenericNotFoundFilter<>));
            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
              .Where(c => c.Name.EndsWith("Filter"))
              .AsPublicImplementedInterfaces(ServiceLifetime.Transient);
        }

        private static void AddCustomJwtService(this IServiceCollection services, TokenOptions tokenOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddCustomSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CityLibrary", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });
        }

        public static void AddCustomLocalizationConfiguration(this IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new ("en-GB"),
                    new ("tr-TR")
                };
                opt.DefaultRequestCulture = new RequestCulture("en-GB");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;

                opt.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });
        }
    }
}
