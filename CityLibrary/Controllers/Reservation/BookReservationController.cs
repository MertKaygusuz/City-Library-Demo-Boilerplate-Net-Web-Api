using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Services.BookReservation.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers.Reservation
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookReservationController : ControllerBase
    {
        private readonly IBookReservationService _bookReservationService;
        public BookReservationController(IBookReservationService bookReservationService)
        {
            _bookReservationService = bookReservationService;
        }

        [HttpPost]
        [ServiceFilter(typeof(IAssigningBookFilter))]
        public async Task AssignBookToMember(AssignBookToMemberDto dto)
        {
            await _bookReservationService.AssignBookToMemberAsync(dto);
        }

        [HttpPost]
        [ServiceFilter(typeof(IUnAssigningBookFilter))]
        public async Task UnAssignBookFromUser(AssignBookToMemberDto dto)
        {
            await _bookReservationService.UnAssignBookFromUserAsync(dto);
        }
    }
}
