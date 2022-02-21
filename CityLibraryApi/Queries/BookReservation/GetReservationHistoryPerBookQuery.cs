using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetReservationHistoryPerBookQuery(ReservationHistoryPerBookDto Dto) : IRequest<IEnumerable<ReservationHistoryBookResponseDto>>
    {
    }
}
