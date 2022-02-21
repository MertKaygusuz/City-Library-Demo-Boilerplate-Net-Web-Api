using CityLibraryApi.Notifications.Test;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.MapperConfigurations;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMembersRepo _membersRepo;
        private readonly IRolesRepo _rolesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly IMemberRolesRepo _memberRolesRepo;
        private readonly IMediator _mediator;
        private readonly HashSet<string> _defaultMemberRoleNames = new() { "User" };
        public TestController(IMembersRepo membersRepo, IRolesRepo rolesRepo, IUnitOfWork unitOfWork, ICustomMapper mapper, IMemberRolesRepo memberRolesRepo, IMediator mediator)
        {
            _membersRepo = membersRepo;
            _rolesRepo = rolesRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memberRolesRepo = memberRolesRepo;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Members>> GetAllMembers()
        {
            return await _membersRepo.GetData().Include(x => x.MemberRoles)
                .Include(x => x.Roles)
                .Include(x => x.ActiveBookReservations)
                .Include(x => x.BookReservationHistories)
                .ThenInclude(x => x.Book)
                .ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Roles>> GetAllRoles()
        {
            await _mediator.Publish(new TestNotification("Address from notification", "Name from notification"));
            return await _rolesRepo.GetData().Include(a => a.MemberRoles).Include(x => x.Members).IgnoreQueryFilters().ToListAsync();
        }

        [HttpGet]
        public async Task DeleteFirstRole()
        {
            var role = await _rolesRepo.GetData(false).FirstOrDefaultAsync();

            _rolesRepo.Delete(role);

            _unitOfWork.Commit();
        }
    }
}
