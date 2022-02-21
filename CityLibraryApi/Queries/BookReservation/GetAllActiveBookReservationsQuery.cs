using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetAllActiveBookReservationsQuery(ActiveBookReservationsFilterDto Filter) : IRequest<IEnumerable<ActiveBookReservationsResponseDto>>
    {
    }
}
