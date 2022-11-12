using CityLibrary.ActionFilters.Base;
using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.Member;
using CityLibraryApi.Services.Member.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.ActionFilters.Classes
{
    public class UserNameCheckFilter : IUserNameCheckFilter
    {
        private readonly IMemberService _memberService;
        public UserNameCheckFilter(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            RegistrationDto modelVal = context.ActionArguments["dto"] as RegistrationDto;
            bool doesExist = await _memberService.DoesEntityExistAsync(modelVal!.UserName);
            if (doesExist)
            {
                var err = new ActionFilterErrorDto();
                err.Errors.Add(nameof(modelVal.UserName), new List<string>() { $"This user name ({modelVal.UserName}) has been already taken." });
                context.Result = new ObjectResult(err)
                {
                    StatusCode = err.status
                };
                return;
            }

            await next();
        }
    }
}
