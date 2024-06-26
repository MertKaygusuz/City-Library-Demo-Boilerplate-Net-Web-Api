﻿using CityLibrary.ActionFilters.Base;
using CityLibraryApi.Dtos.BookReservation;
using CityLibraryApi.Services.Book.Interfaces;
using CityLibraryApi.Services.BookReservation.Interfaces;
using CityLibraryApi.Services.Member.Interfaces;
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
        private readonly IBookReservationService _bookReservationService;
        private readonly IBookService _bookService;
        public ReportController(IBookReservationService bookReservationService, IBookService bookService)
        {
            _bookReservationService = bookReservationService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<int> GetDistinctBookTitleNumber()
        {
            return await _bookService.GetNumberOfDistinctTitleAsync();
        }

        [HttpGet]
        public async Task<int> GetDistinctAuthorsNumber()
        {
            return await _bookService.GetNumberOfAuthorsFromBookTableAsync();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ActiveBookReservationsResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ActiveBookReservationsResponseDto>> GetActiveBookReservations(ActiveBookReservationsFilterDto dto)
        {
            return await _bookReservationService.GetAllActiveBookReservationsAsync(dto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>> GetNumberOfBooksPerTitleAndEditionNumber()
        {
            return await _bookReservationService.GetNumberOfBooksPerTitleAndEditionNumberAsync();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NumberOfBooksReservedByMembersResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<NumberOfBooksReservedByMembersResponseDto>> GetNumberOfBooksReservedPerMembers()
        {
            return await _bookReservationService.GetNumberOfBooksReservedPerMembersAsync();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ReservationHistoryBookResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryPerBook(ReservationHistoryPerBookDto dto)
        {
            return await _bookReservationService.GetReservationHistoryPerBookAsync(dto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ReservationHistoryMemberResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryPerMember(ReservationHistoryPerMemberDto dto)
        {
            return await _bookReservationService.GetReservationHistoryPerMemberAsync(dto);
        }

        [HttpPost]
        [ServiceFilter(typeof(GenericNotFoundFilter<IBookService>))]
        [ProducesResponseType(typeof(IEnumerable<ReservationHistoryBookResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ReservationHistoryBookResponseDto>> GetReservationHistoryByBook(ReservationHistoryBookDto dto)
        {
            return await _bookReservationService.GetReservationHistoryByBookAsync(dto);
        }

        [HttpPost]
        [ServiceFilter(typeof(GenericNotFoundFilter<IMemberService>))]
        [ProducesResponseType(typeof(IEnumerable<ReservationHistoryMemberResponseDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ReservationHistoryMemberResponseDto>> GetReservationHistoryByMember(ReservationHistoryMemberDto dto)
        {
            return await _bookReservationService.GetReservationHistoryByMemberAsync(dto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<DateTime>), StatusCodes.Status200OK)]
        public IEnumerable<DateTime> GetReservedBooksEstimatedReturnDates(ReservedBookEstimatedReturnDatesDto dto)
        {
            return _bookReservationService.GetReservedBooksEstimatedReturnDates(dto);
        }
    }
}
