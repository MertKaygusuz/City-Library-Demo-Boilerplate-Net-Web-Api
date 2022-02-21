using CityLibraryApi.Dtos.BookReservation;
using MediatR;

namespace CityLibraryApi.Commands.BookReservation
{
    public record AssignBookToMemberCommand(AssignBookToMemberDto Dto) : IRequest
    {
    }
}
