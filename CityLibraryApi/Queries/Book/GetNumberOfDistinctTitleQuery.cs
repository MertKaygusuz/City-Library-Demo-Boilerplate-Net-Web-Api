using MediatR;

namespace CityLibraryApi.Queries.Book
{
    public record GetNumberOfDistinctTitleQuery : IRequest<int>
    {
    }
}
