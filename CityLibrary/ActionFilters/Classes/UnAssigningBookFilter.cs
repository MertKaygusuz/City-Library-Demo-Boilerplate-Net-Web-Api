using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Services.BookReservation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CityLibraryInfrastructure.ExceptionHandling.Dtos;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibrary.ActionFilters.Classes
{
    public class UnAssigningBookFilter : IUnAssigningBookFilter
    {
        private readonly IBookReservationService _bookReservationService;
        private readonly IStringLocalizer<ActionFiltersResource> _localizer;

        public UnAssigningBookFilter(IBookReservationService bookReservationService, IStringLocalizer<ActionFiltersResource> localizer)
        {
            _bookReservationService = bookReservationService;
            _localizer = localizer;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AssignBookToMemberDto modelVal = context.ActionArguments["dto"] as AssignBookToMemberDto;
            var memberExistTask = _bookReservationService.CheckIfMemberExistsAsync(modelVal!.UserName);
            var bookExistTask = _bookReservationService.CheckIfBookExistsAsync(modelVal.BookId);

            await Task.WhenAll(memberExistTask, bookExistTask); //parallel request to db.
            bool memberExist = memberExistTask.Result;
            bool bookExist = bookExistTask.Result;


            if (!(memberExist && bookExist))
            {
                var err = new ErrorDto();
                if (!memberExist)
                    err.Errors.Add(nameof(modelVal.UserName), new List<string>() { _localizer["User_Name_Not_Exist"] });

                if (!bookExist)
                    err.Errors.Add(nameof(modelVal.BookId), new List<string>() { _localizer["Book_Not_Exist"] });

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
