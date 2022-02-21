using CityLibraryApi.Commands.Auth;
using CityLibraryApi.Commands.Token;
using CityLibraryApi.Dtos.Token;
using CityLibraryApi.Dtos.Token.Records;
using CityLibraryApi.Queries.Member;
using CityLibraryApi.Queries.Token;
using CityLibraryInfrastructure.Entities.Cache;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Commands.Auth
{
    public class AuthCommandHandlers : IRequestHandler<LoginCommand, ReturnTokenRecord>,
                                       IRequestHandler<LogoutCommand>,
                                       IRequestHandler<RefreshLoginTokenCommand, ReturnTokenRecord>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AuthCommandHandlers(IMediator mediator, ILogger<AuthCommandHandlers> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<ReturnTokenRecord> Handle(RefreshLoginTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshTokens oldToken = await _mediator.Send(new RefreshTokenGetByKeyQuery(request.RefreshTokenKey), cancellationToken);

            if (oldToken is null)
                throw new CustomBusinessException("Refresh token could not be found!");

            if (DateTime.Compare(DateTime.Now, oldToken.DueTime) > 1)
                throw new CustomStatusException("Session timeout! Please relogin.", 401);


            CreateTokenDto createTokenDto = new()
            {
                UserName = oldToken.UserName,
                FullName = oldToken.FullName,
                UserRoleNames = oldToken.UserRoleNames
            };
            CreateTokenResultDto newToken = await _mediator.Send(new CreateTokenCommand(createTokenDto), cancellationToken);

            var newRefreshToken = new RefreshTokens()
            {
                ClientAgent = newToken.ClientAgent,
                ClientIp = newToken.ClientIp,
                TokenKey = newToken.RefreshTokenKey,
                FullName = oldToken.FullName, // get some old values from cache
                UserName = oldToken.UserName,
                UserRoleNames = oldToken.UserRoleNames
            };

            await _mediator.Send(new RefreshTokenCreateOrUpdateCommand(newRefreshToken), cancellationToken);
            await _mediator.Send(new RefreshTokenDeleteCommand(request.RefreshTokenKey), cancellationToken);

            _logger.LogInformation($"Refresh login was executed successfully. UserName: {oldToken.UserName}," +
                                    $" IP: {newToken.ClientIp}, Agent: {newToken.ClientAgent}");

            return new ReturnTokenRecord(newToken.AccessToken, newToken.RefreshTokenKey);
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RefreshTokenDeleteCommand(request.TokenKey), cancellationToken);
            return Unit.Value;
        }

        public async Task<ReturnTokenRecord> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var member = await _mediator.Send(new GetMemberByUserNameQuery(request.LoginDto.UserName), cancellationToken);
            if (member is null || request.LoginDto.Password.VerifyPasswordHash(member.Password) is false)
                throw new CustomBusinessException("User name or password is incorrect.");

            CreateTokenDto createTokenDto = new()
            {
                FullName = member.FullName,
                UserName = member.UserName,
                UserRoleNames = member.Roles.Select(x => x.RoleName)
            };

            var token = await _mediator.Send(new CreateTokenCommand(createTokenDto), cancellationToken);

            var newRefreshToken = new RefreshTokens()
            {
                ClientAgent = token.ClientAgent,
                ClientIp = token.ClientIp,
                TokenKey = token.RefreshTokenKey,
                FullName = member.FullName,
                UserName = member.UserName,
                UserRoleNames = createTokenDto.UserRoleNames
            };

            await _mediator.Send(new RefreshTokenCreateOrUpdateCommand(newRefreshToken), cancellationToken);

            _logger.LogInformation($"Refresh login was executed successfully. UserName: {member.UserName}," +
                                    $" IP: {token.ClientIp}, Agent: {token.ClientAgent}");

            return new ReturnTokenRecord(token.AccessToken, token.RefreshTokenKey);
        }
    }
}
