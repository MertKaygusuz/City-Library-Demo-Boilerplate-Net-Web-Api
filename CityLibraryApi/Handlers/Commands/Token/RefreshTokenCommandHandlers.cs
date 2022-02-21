using CityLibraryApi.Commands.Token;
using CityLibraryInfrastructure.AppSettings;
using CityLibraryInfrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Commands.Token
{
    public class RefreshTokenCommandHandlers : AsyncRequestHandler<RefreshTokenDeleteCommand>, IRequestHandler<RefreshTokenCreateOrUpdateCommand>
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IDistributedCache _cache;
        public RefreshTokenCommandHandlers(IOptions<AppSetting> options, IDistributedCache cache)
        {
            _tokenOptions = options.Value.TokenOptions;
            _cache = cache;
        }

        public async Task<Unit> Handle(RefreshTokenCreateOrUpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.AutoExpiration)
                request.Token.DueTime = DateTime.Now.AddHours(_tokenOptions.RefreshTokenExpiration);
            await _cache.SaveRecordAsync(request.Token.TokenKey, request.Token, TimeSpan.FromHours(_tokenOptions.RefreshTokenExpiration));
            return Unit.Value;
        }

        protected override async Task Handle(RefreshTokenDeleteCommand request, CancellationToken cancellationToken)
        {
            await _cache.RemoveRecordAsync(request.Key);
        }
    }
}
