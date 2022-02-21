using MediatR;

namespace CityLibraryApi.Queries.Book
{
    public record GetNumberOfAuthorsFromBookTableQuery : IRequest<int>
    {
    }
}
