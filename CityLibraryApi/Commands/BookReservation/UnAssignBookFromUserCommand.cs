using CityLibraryApi.Dtos.BookReservation;
using MediatR;

namespace CityLibraryApi.Commands.BookReservation
{
    public record UnAssignBookFromUserCommand(AssignBookToMemberDto Dto) : IRequest
    {
    }
}
