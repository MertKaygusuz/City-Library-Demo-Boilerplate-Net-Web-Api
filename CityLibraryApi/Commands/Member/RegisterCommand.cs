using CityLibraryApi.Dtos.Member;
using MediatR;

namespace CityLibraryApi.Commands.Member
{
    public record RegisterCommand(RegistrationDto RegistrationDto) : IRequest<string>
    {
    }
}
