using CityLibraryApi.Commands.Token;
using CityLibraryApi.Dtos.Token;
using CityLibraryInfrastructure.AppSettings;
using static CityLibraryInfrastructure.Statics.Methods.TokenRelated;
using static CityLibraryInfrastructure.Statics.Methods.RandomGenerators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CityLibraryApi.Handlers.Commands.Token
{
    public class AccessTokenCommandHandlers : IRequestHandler<CreateTokenCommand, CreateTokenResultDto>
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessTokenCommandHandlers(IOptions<AppSetting> options, IHttpContextAccessor httpContextAccessor)
        {
            _tokenOptions = options.Value.TokenOptions;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateTokenResultDto> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var accessTokenExpiration = DateTime.Now.AddHours(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddHours(_tokenOptions.RefreshTokenExpiration);
            var securityKey = GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(dto),
                 signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

            var result = new CreateTokenResultDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshTokenKey = CreateRefreshTokenKey(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
                ClientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(), //TODO: simplify
                ClientAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"],
                MemberName = dto.UserName
            };

            return await Task.FromResult(result);
        }
        #region private methods
        private static string CreateRefreshTokenKey()
        {
            return RandomStringFromBytes();
        }

        private IEnumerable<Claim> GetClaims(CreateTokenDto dto)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dto.UserName),
                new Claim(ClaimTypes.Name,dto.FullName),
                new Claim(JwtRegisteredClaimNames.Aud, _tokenOptions.Audience)
            };

            userClaims.AddRange(dto.UserRoleNames.Select(roleName => new Claim(ClaimTypes.Role, roleName)));

            return userClaims;
        }
        #endregion
    }
}
