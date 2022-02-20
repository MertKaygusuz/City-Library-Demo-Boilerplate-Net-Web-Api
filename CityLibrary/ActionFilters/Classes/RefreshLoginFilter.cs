using CityLibrary.ActionFilters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.ActionFilters.Classes
{
    public class RefreshLoginFilter : IRefreshLoginFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string refreshToken = context.ActionArguments["refreshToken"] as string;

            if (string.IsNullOrEmpty(refreshToken))
            {
                context.Result = new ObjectResult($"Refresh token is empty.")
                {
                    StatusCode = 400
                };
                return;
            }
            
            await next();
        }
    }
}
