using CityLibraryApi.Dtos.Token;
using CityLibraryApi.Services.Token.Interfaces;
using CityLibraryInfrastructure.AppSettings;
using static CityLibraryInfrastructure.Statics.Methods.TokenRelated;
using static CityLibraryInfrastructure.Statics.Methods.RandomGenerators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CityLibraryApi.Services.Token.Classes
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly TokenOptions _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccessTokenService(IOptions<AppSetting> options, IHttpContextAccessor httpContextAccessor)
        {
            _tokenOptions = options.Value.TokenOptions;
            _httpContextAccessor = httpContextAccessor;
        }
        public CreateTokenResultDto CreateToken(CreateTokenDto dto)
        {
            var accessTokenExpiration = DateTime.Now.AddHours(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddHours(_tokenOptions.RefreshTokenExpiration);
            var securityKey = GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(dto),
                 signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

            return new CreateTokenResultDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                RefreshTokenKey = CreateRefreshTokenKey(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
                ClientIp = Convert.ToString(_httpContextAccessor.HttpContext!.Connection.RemoteIpAddress), //TODO: simplify
                ClientAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"],
                MemberName = dto.UserName
            };
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
