using CityLibraryApi.Dtos.Authentication;
using CityLibraryApi.Dtos.Token;
using CityLibraryApi.Dtos.Token.Records;
using CityLibraryApi.Services.Member.Interfaces;
using CityLibraryApi.Services.Token.Interfaces;
using CityLibraryInfrastructure.Entities.Cache;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryApi.Services.Token.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IMemberService _memberService;
        private readonly IStringLocalizer<ExceptionsResource> _localizer;
        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IStringLocalizer<ExceptionsResource> localizer,
            IMemberService memberService,
            IRefreshTokenService refreshTokenService,
            IAccessTokenService accessTokenService)
        {
            _logger = logger;
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
            _memberService = memberService;
            _localizer = localizer;
        }
        public async Task<ReturnTokenRecord> LoginAsync(LoginDto loginDto)
        {
            var member = await _memberService.GetMemberByUserNameAsync(loginDto.UserName);
            if (member is null || loginDto.Password.VerifyPasswordHash(member.Password) is false)
                throw new CustomBusinessException(_localizer["Login_Fail"]);

            CreateTokenDto createTokenDto = new()
            {
                FullName = member.FullName,
                UserName = member.UserName,
                UserRoleNames = member.Roles.Select(x => x.RoleName)
            };

            var token = _accessTokenService.CreateToken(createTokenDto);

            var newRefreshToken = new RefreshTokens()
            {
                ClientAgent = token.ClientAgent,
                ClientIp = token.ClientIp,
                TokenKey = token.RefreshTokenKey,
                FullName = member.FullName,
                UserName = member.UserName,
                UserRoleNames = createTokenDto.UserRoleNames
            };

            await _refreshTokenService.CreateOrUpdateAsync(newRefreshToken);

            _logger.LogInformation($"Refresh login was executed successfully. UserName: {member.UserName}," +
                                    $" IP: {token.ClientIp}, Agent: {token.ClientAgent}");

            return new ReturnTokenRecord(token.AccessToken, token.RefreshTokenKey);
        }

        public async Task LogoutAsync(string tokenKey)
        {
            await _refreshTokenService.DeleteAsync(tokenKey);
        }

        public async Task<ReturnTokenRecord> RefreshLoginTokenAsync(string refreshTokenKey)
        {
            RefreshTokens oldToken = await _refreshTokenService.GetByKeyAsync(refreshTokenKey);

            if (oldToken is null)
                throw new CustomBusinessException("Refresh token could not be found!");

            if (DateTime.Compare(DateTime.Now, oldToken.DueTime) > 1)
                throw new CustomStatusException(_localizer["Session_Timeout"], 401);
            

            CreateTokenDto createTokenDto = new()
            {
                UserName = oldToken.UserName,
                FullName = oldToken.FullName,
                UserRoleNames = oldToken.UserRoleNames
            };
            CreateTokenResultDto newToken = _accessTokenService.CreateToken(createTokenDto);

            var newRefreshToken = new RefreshTokens()
            {
                ClientAgent = newToken.ClientAgent,
                ClientIp = newToken.ClientIp,
                TokenKey = newToken.RefreshTokenKey,
                FullName = oldToken.FullName, // get some old values from cache
                UserName = oldToken.UserName,
                UserRoleNames = oldToken.UserRoleNames
            };

            await _refreshTokenService.CreateOrUpdateAsync(newRefreshToken);
            await _refreshTokenService.DeleteAsync(refreshTokenKey);

            _logger.LogInformation($"Refresh login was executed successfully. UserName: {oldToken.UserName}," +
                                    $" IP: {newToken.ClientIp}, Agent: {newToken.ClientAgent}");

            return new ReturnTokenRecord(newToken.AccessToken, newToken.RefreshTokenKey);
        }
    }
}
