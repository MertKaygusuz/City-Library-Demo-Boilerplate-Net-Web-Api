using CityLibraryInfrastructure.Entities.Cache;
using MediatR;

namespace CityLibraryApi.Commands.Token
{
    public record RefreshTokenCreateOrUpdateCommand(RefreshTokens Token, bool AutoExpiration = true) : IRequest
    {
    }
}
