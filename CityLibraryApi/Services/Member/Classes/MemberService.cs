using CityLibraryApi.Dtos.Member;
using CityLibraryApi.Services.Member.Interfaces;
using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Extensions;
using CityLibraryInfrastructure.MapperConfigurations;
using CityLibraryInfrastructure.Repositories;
using static CityLibraryInfrastructure.Extensions.TokenExtensions.AccesInfoFromToken;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLibraryInfrastructure.ExceptionHandling;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryApi.Services.Member.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IMembersRepo _membersRepo;
        private readonly IRolesRepo _rolesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly IStringLocalizer<ExceptionsResource> _localizer;
        private readonly HashSet<string> _defaultMemberRoleNames = new() { "User" };
        public MemberService(IMembersRepo membersRepo, 
                             IRolesRepo rolesRepo,
                             IStringLocalizer<ExceptionsResource> localizer,
                             IUnitOfWork unitOfWork, 
                             ICustomMapper mapper)
        {
            _membersRepo = membersRepo;
            _rolesRepo = rolesRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Members> GetMemberByUserNameAsync(string userName)
        {
            return await _membersRepo.GetDataWithLinqExp(x => x.UserName == userName, "Roles")
                                     .SingleOrDefaultAsync();
        }

        public async Task<string> RegisterAsync(RegistrationDto registrationDto)
        {
            Members newMember = _mapper.Map<RegistrationDto, Members>(registrationDto);
            newMember.Password.CreatePasswordHash(out string hashedPass);
            newMember.Password = hashedPass;

            var roles = _rolesRepo.GetLocalViewWithLinqExp(x => _defaultMemberRoleNames.Contains(x.RoleName));
            newMember.Roles = new List<Roles>();
            
            newMember.Roles.AddRange(roles);

            await _membersRepo.InsertAsync(newMember);

            await _unitOfWork.CommitAsync();

            return newMember.UserName;
        }

        public async Task<bool> DoesEntityExistAsync(IConvertible Id)
        {
            return await _membersRepo.DoesEntityExistAsync(Id as string);
        }

        public async Task MemberSelfUpdateAsync(MemberSelfUpdateDto selfUpdateDto)
        {
            string myUserName = GetMyUserId();
            if (string.IsNullOrEmpty(myUserName) || !await _membersRepo.DoesEntityExistAsync(myUserName))
                throw new CustomBusinessException(_localizer["Member_Not_Found"]);

            var registrationDto = _mapper.Map<MemberSelfUpdateDto, RegistrationDto>(selfUpdateDto);
            registrationDto.UserName = myUserName;
            await AdminUpdateMemberAsync(registrationDto);
        }

        public async Task AdminUpdateMemberAsync(RegistrationDto registrationDto)
        {
            Members theMember = await _membersRepo.GetByIdAsync(registrationDto.UserName);
            theMember.FullName = registrationDto.FullName;
            theMember.BirthDate = registrationDto.BirthDate;
            theMember.Address = registrationDto.Address;
            registrationDto.Password.CreatePasswordHash(out string hashedPass);
            theMember.Password = hashedPass;

            await _unitOfWork.CommitAsync();
        }
    }
}
