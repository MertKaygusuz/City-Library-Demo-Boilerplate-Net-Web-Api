using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.Member;
using CityLibraryApi.Services.Member.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CityLibraryInfrastructure.ExceptionHandling.Dtos;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibrary.ActionFilters.Classes
{
    public class UserNameCheckFilter : IUserNameCheckFilter
    {
        private readonly IMemberService _memberService;
        private readonly IStringLocalizer<ActionFiltersResource> _localizer;
        public UserNameCheckFilter(IMemberService memberService, IStringLocalizer<ActionFiltersResource> localizer)
        {
            _memberService = memberService;
            _localizer = localizer;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            RegistrationDto modelVal = context.ActionArguments["dto"] as RegistrationDto;
            bool doesExist = await _memberService.DoesEntityExistAsync(modelVal!.UserName);
            if (doesExist)
            {
                var err = new ErrorDto();
                err.Errors.Add(nameof(modelVal.UserName), new List<string>() { string.Format(_localizer["User_Name_Check"], modelVal.UserName) });
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
