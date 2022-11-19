using CityLibrary.ActionFilters.Base;
using CityLibrary.ActionFilters.Interfaces;
using CityLibraryApi.Dtos.Member;
using CityLibraryApi.Services.Member.Interfaces;
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
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        [ServiceFilter(typeof(IUserNameCheckFilter))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<string> Register(RegistrationDto dto)
        {
            return await _memberService.RegisterAsync(dto);
        }

        [HttpPut]
        [ServiceFilter(typeof(GenericNotFoundFilter<IMemberService>))]
        [Authorize(Roles = "Admin")]
        public async Task AdminUpdateMember(RegistrationDto dto)
        {
            await _memberService.AdminUpdateMemberAsync(dto);
        }

        [HttpPut]
        public async Task MemberSelfUpdate(MemberSelfUpdateDto dto)
        {
            await _memberService.MemberSelfUpdateAsync(dto);
        }
    }
}
