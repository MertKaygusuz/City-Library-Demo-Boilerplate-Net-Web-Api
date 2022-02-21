using CityLibraryApi.Dtos.BookReservation;
using MediatR;
using System.Collections.Generic;

namespace CityLibraryApi.Queries.BookReservation
{
    public record GetNumberOfBooksPerTitleAndEditionNumberQuery : IRequest<IEnumerable<NumberOfBooksPerTitleAndEditionNumberResponseDto>>
    {
    }
}
