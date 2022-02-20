using CityLibraryApi.Dtos.Authentication;
using CityLibraryApi.Dtos.Token.Records;
using System.Threading.Tasks;

namespace CityLibraryApi.Services.Token.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ReturnTokenRecord> LoginAsync(LoginDto loginDto);

        Task<ReturnTokenRecord> RefreshLoginTokenAsync(string refreshTokenKey);

        Task LogoutAsync(string tokenKey);
    }
}
