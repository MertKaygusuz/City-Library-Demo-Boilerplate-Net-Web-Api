using CityLibraryApi.Services.Token.Interfaces;
using CityLibraryInfrastructure.AppSettings;
using CityLibraryInfrastructure.Entities.Cache;
using CityLibraryInfrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CityLibraryApi.Services.Token.Classes
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IDistributedCache _cache;
        public RefreshTokenService(IOptions<AppSetting> options, IDistributedCache cache)
        {
            _tokenOptions = options.Value.TokenOptions;
            _cache = cache;
        }

        public async Task DeleteAsync(string key)
        {
            await _cache.RemoveRecordAsync(key);
        }

        public async Task<bool> DoesEntityExistAsync(IConvertible Id)
        {
            return (await GetByKeyAsync(Id as string)) is not null;
        }

        public async Task<RefreshTokens> GetByKeyAsync(string key)
        {
            return await _cache.GetRecordAsync<RefreshTokens>(key);
        }

        public async Task CreateOrUpdateAsync(RefreshTokens token, bool autoExpiration = true)
        {
            if (autoExpiration)
                token.DueTime = DateTime.Now.AddHours(_tokenOptions.RefreshTokenExpiration);
            await _cache.SaveRecordAsync(token.TokenKey, token, TimeSpan.FromHours(_tokenOptions.RefreshTokenExpiration));
        }
    }
}
