using CityLibraryApi.Dtos.Book;
using MediatR;

namespace CityLibraryApi.Commands.Book
{
    public record BookDeleteCommand(DeleteBookDto Dto) : IRequest
    {
    }
}
