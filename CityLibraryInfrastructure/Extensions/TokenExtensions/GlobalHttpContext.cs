using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CityLibraryInfrastructure.Extensions.TokenExtensions
{
   
    public static class GlobalHttpContext
    {
        public static IHttpContextAccessor _contextAccessor;

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }

    public static class StaticHttpContextExtensions
    {
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            GlobalHttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
    
}
