using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetNumberOfBooksReservedPerMembersQuery : IRequest<IEnumerable<NumberOfBooksReservedByMembersResponseDto>>
    {
    }
}
