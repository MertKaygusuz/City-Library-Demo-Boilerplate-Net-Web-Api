using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.Authentication;
using CityLibraryApi.Dtos.Token.Records;
using CityLibraryApi.Services.Token.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ReturnTokenRecord> Login(LoginDto dto)
        {
            return await _authenticationService.LoginAsync(dto);
        }

        [HttpPost]
        public async Task Logout([FromForm] string refreshToken)
        {
            await _authenticationService.LogoutAsync(refreshToken);
        }

        [HttpPut]
        [ServiceFilter(typeof(IRefreshLoginFilter))]
        public async Task<ReturnTokenRecord> ReLoginWithRefreshToken([FromForm] string refreshToken)
        {
            return await _authenticationService.RefreshLoginTokenAsync(refreshToken);
        }
    }
}
