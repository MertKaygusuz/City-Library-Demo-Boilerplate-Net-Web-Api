using CityLibraryInfrastructure.Entities;
using MediatR;
using System.Collections.Generic;


namespace CityLibraryApi.Queries.Book
{
    public record GetAllBooksQuery : IRequest<IEnumerable<Books>>
    {
    }
}
