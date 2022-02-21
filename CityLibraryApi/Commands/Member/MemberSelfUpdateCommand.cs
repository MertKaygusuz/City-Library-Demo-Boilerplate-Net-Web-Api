using CityLibraryApi.Dtos.Member;
using MediatR;

namespace CityLibraryApi.Commands.Member
{
    public record MemberSelfUpdateCommand(MemberSelfUpdateDto SelfUpdateDto) : IRequest
    {
    }
}
