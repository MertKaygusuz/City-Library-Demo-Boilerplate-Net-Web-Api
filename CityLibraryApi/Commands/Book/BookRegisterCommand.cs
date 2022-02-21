using CityLibraryApi.Dtos.Book;
using MediatR;

namespace CityLibraryApi.Commands.Book
{
    public record BookRegisterCommand(RegisterBookDto Dto) : IRequest<int>
    {
    }
}
