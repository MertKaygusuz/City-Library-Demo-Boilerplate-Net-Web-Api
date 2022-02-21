using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetReservationHistoryPerMemberQuery(ReservationHistoryPerMemberDto Dto) : IRequest<IEnumerable<ReservationHistoryMemberResponseDto>>
    {
    }
}
