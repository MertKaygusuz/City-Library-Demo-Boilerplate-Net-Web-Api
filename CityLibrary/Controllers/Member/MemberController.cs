using CityLibrary.ActionFilters.Base;
using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Commands.Member;
using CityLibraryApi.Dtos.Member;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers.Member
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Register(RegistrationDto dto)
        {
            return await _mediator.Send(new RegisterCommand(dto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task AdminUpdateMember(RegistrationDto dto)
        {
            await _mediator.Send(new AdminUpdateMemberCommand(dto));
        }

        [HttpPut]
        public async Task MemberSelfUpdate(MemberSelfUpdateDto dto)
        {
            await _mediator.Send(new MemberSelfUpdateCommand(dto));
        }
    }
}
