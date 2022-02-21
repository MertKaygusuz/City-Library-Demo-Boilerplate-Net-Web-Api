using MediatR;

namespace CityLibraryApi.Commands.Auth
{
    public record LogoutCommand(string TokenKey) : IRequest
    {
    }
}
