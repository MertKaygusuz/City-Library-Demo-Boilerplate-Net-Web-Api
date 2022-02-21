using CityLibraryApi.Dtos.Authentication;
using CityLibraryApi.Dtos.Token.Records;
using MediatR;

namespace CityLibraryApi.Commands.Auth
{
    public record LoginCommand(LoginDto LoginDto) : IRequest<ReturnTokenRecord>
    {
    }
}
