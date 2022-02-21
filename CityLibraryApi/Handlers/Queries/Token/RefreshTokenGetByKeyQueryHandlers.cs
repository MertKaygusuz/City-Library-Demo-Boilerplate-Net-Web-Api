using CityLibraryApi.Queries.Token;
using CityLibraryInfrastructure.Entities.Cache;
using CityLibraryInfrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Queries.Token
{
    public class RefreshTokenGetByKeyQueryHandlers : IRequestHandler<RefreshTokenGetByKeyQuery, RefreshTokens>
    {
        private readonly IDistributedCache _cache;
        public RefreshTokenGetByKeyQueryHandlers(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<RefreshTokens> Handle(RefreshTokenGetByKeyQuery request, CancellationToken cancellationToken)
        {
            return await _cache.GetRecordAsync<RefreshTokens>(request.Key);
        }
    }
}
