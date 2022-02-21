using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetReservedBooksEstimatedReturnDatesQuery(ReservedBookEstimatedReturnDatesDto Dto) : IRequest<IEnumerable<DateTime>>
    {
    }
}
