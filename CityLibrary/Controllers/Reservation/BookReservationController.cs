using CityLibraryApi.Commands.BookReservation;
using CityLibraryApi.Dtos.BookReservation;
using MediatR;
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
        private readonly IMediator _mediator;
        public BookReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task AssignBookToMember(AssignBookToMemberDto dto)
        {
            await _mediator.Send(new AssignBookToMemberCommand(dto));
        }

        [HttpPost]
        public async Task UnAssignBookFromUser(AssignBookToMemberDto dto)
        {
            await _mediator.Send(new UnAssignBookFromUserCommand(dto));
        }
    }
}
