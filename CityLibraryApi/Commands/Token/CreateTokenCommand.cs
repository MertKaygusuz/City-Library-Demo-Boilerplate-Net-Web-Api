using CityLibraryApi.Dtos.Token;
using MediatR;

namespace CityLibraryApi.Commands.Token
{
    public record CreateTokenCommand(CreateTokenDto Dto) : IRequest<CreateTokenResultDto>
    {
    }
}
