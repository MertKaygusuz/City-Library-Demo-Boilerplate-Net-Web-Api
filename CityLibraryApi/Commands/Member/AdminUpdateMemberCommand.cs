using CityLibraryApi.Dtos.Member;
using MediatR;

namespace CityLibraryApi.Commands.Member
{
    public record AdminUpdateMemberCommand(RegistrationDto RegistrationDto) : IRequest
    {
    }
}
