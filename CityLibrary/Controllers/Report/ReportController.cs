using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Queries.Book;
using CityLibraryApi.Queries.BookReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers.Report
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<int> GetDistinctBookTitleNumber()
        {
            return await _mediator.Send(new GetNumberOfDistinctTitleQuery());
        }

        [HttpGet]
        public async Task<int> GetDistinctAuthorsNumber()
        {
            return await _mediator.Send(new GetNumberOfAuthorsFromBookTableQuery());
        }

        [HttpPost]
        public async Task<IEnumerable<ActiveBookReservationsResponseDto>> GetActiveBookReservations(ActiveBookReservationsFilterDto dto)
        {
            return await _mediator.Send(new GetAllActiveBookReservationsQuery(dto));
        }

        [HttpGet]
        public async Task<IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>> GetNumberOfBooksPerTitleAndEditionNumber()
        {
            return await _mediator.Send(new GetNumberOfBooksPerTitleAndEditionNumberQuery());
        }

        [HttpGet]
        public async Task<IEnumerable<NumberOfBooksReservedByMembersResponseDto>> GetNumberOfBooksReservedPerMembers()
        {
            return await _mediator.Send(new GetNumberOfBooksReservedPerMembersQuery());
        }

        [HttpPost]
        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryPerBook(ReservationHistoryPerBookDto dto)
        {
            return await _mediator.Send(new GetReservationHistoryPerBookQuery(dto));
        }

        [HttpPost]
        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryPerMember(ReservationHistoryPerMemberDto dto)
        {
            return await _mediator.Send(new GetReservationHistoryPerMemberQuery(dto));
        }

        [HttpPost]
        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryByBook(ReservationHistoryBookDto dto)
        {
            return await _mediator.Send(new GetReservationHistoryByBookQuery(dto));
        }

        [HttpPost]
        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryByMember(ReservationHistoryMemberDto dto)
        {
            return await _mediator.Send(new GetReservationHistoryByMemberQuery(dto));
        }

        [HttpPost]
        public async Task<IEnumerable<DateTime>> GetReservedBooksEstimatedReturnDates(ReservedBookEstimatedReturnDatesDto dto)
        {
            return await _mediator.Send(new GetReservedBooksEstimatedReturnDatesQuery(dto));
        }
    }
}
