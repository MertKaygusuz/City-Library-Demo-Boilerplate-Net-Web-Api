using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetReservationHistoryByBookQuery(ReservationHistoryBookDto Dto) : IRequest<IEnumerable<ReservationHistoryBookResponseDto>>
    {
    }
}
