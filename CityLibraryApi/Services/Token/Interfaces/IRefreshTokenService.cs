using CityLibraryInfrastructure.BaseInterfaces;
using CityLibraryInfrastructure.Entities.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryApi.Services.Token.Interfaces
{
    public interface IRefreshTokenService : IBaseCheckService
    {
        Task CreateOrUpdateAsync(RefreshTokens token, bool autoExpiration = true);

        Task DeleteAsync(string key);

        Task<RefreshTokens> GetByKeyAsync(string key);
    }
}
