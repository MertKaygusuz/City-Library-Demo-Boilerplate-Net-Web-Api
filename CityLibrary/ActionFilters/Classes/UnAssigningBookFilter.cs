using CityLibrary.ActionFilters.Base;
using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Services.BookReservation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.ActionFilters.Classes
{
    public class UnAssigningBookFilter : IUnAssigningBookFilter
    {
        private readonly IBookReservationService _bookReservationService;
        public UnAssigningBookFilter(IBookReservationService bookReservationService)
        {
            _bookReservationService = bookReservationService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AssignBookToMemberDto modelVal = context.ActionArguments["dto"] as AssignBookToMemberDto;
            var memberExistTask = _bookReservationService.CheckIfMemberExistsAsync(modelVal.UserName);
            var bookExistTask = _bookReservationService.CheckIfBookExistsAsync(modelVal.BookId);

            await Task.WhenAll(memberExistTask, bookExistTask); //parallel request to db.
            bool memberExist = memberExistTask.Result;
            bool bookExist = bookExistTask.Result;


            if (!(memberExist && bookExist))
            {
                var err = new ActionFilterErrorDto();
                if (!memberExist)
                    err.Errors.Add(nameof(modelVal.UserName), new List<string>() { $"User name does not exist on system." });

                if (!bookExist)
                    err.Errors.Add(nameof(modelVal.BookId), new List<string>() { $"Book does not exist on system." });

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
