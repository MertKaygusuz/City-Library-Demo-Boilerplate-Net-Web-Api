using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Commands.Auth;
using CityLibraryApi.Dtos.Authentication;
using CityLibraryApi.Dtos.Token.Records;
using MediatR;
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
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ReturnTokenRecord> Login(LoginDto dto)
        {
            return await _mediator.Send(new LoginCommand(dto));
        }

        [HttpPost]
        public async Task Logout([FromForm] string refreshToken)
        {
            await _mediator.Send(new LogoutCommand(refreshToken));
        }

        [HttpPut]
        [ServiceFilter(typeof(IRefreshLoginFilter))]
        public async Task<ReturnTokenRecord> ReLoginWithRefreshToken([FromForm] string refreshToken)
        {
            return await _mediator.Send(new RefreshLoginTokenCommand(refreshToken));
        }
    }
}
