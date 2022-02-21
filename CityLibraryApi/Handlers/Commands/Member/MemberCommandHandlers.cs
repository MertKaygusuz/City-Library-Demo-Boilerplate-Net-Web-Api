using CityLibraryApi.Commands.Member;
using CityLibraryApi.Dtos.Member;
using CityLibraryDomain.UnitOfWorks;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Extensions;
using CityLibraryInfrastructure.MapperConfigurations;
using CityLibraryInfrastructure.Repositories;
using static CityLibraryInfrastructure.TokenExtensions.AccesInfoFromToken;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CityLibraryInfrastructure.ExceptionHandling;
using Microsoft.EntityFrameworkCore;

namespace CityLibraryApi.Handlers.Commands.Member
{
    public class MemberCommandHandlers : IRequestHandler<RegisterCommand, string>,
                                         IRequestHandler<AdminUpdateMemberCommand>,
                                         IRequestHandler<MemberSelfUpdateCommand>
    {
        private readonly IMembersRepo _membersRepo;
        private readonly IRolesRepo _rolesRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomMapper _mapper;
        private readonly HashSet<string> _defaultMemberRoleNames = new() { "User" };
        public MemberCommandHandlers(IMembersRepo membersRepo, IRolesRepo rolesRepo, IUnitOfWork unitOfWork, ICustomMapper mapper)
        {
            _membersRepo = membersRepo;
            _rolesRepo = rolesRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(MemberSelfUpdateCommand request, CancellationToken cancellationToken)
        {
            string myUserName = GetMyUserId();
            if (string.IsNullOrEmpty(myUserName) || !await _membersRepo.DoesEntityExistAsync(myUserName))
                throw new CustomBusinessException("Member could not be found. Ensure that you are loged in.");

            var registrationDto = _mapper.Map<MemberSelfUpdateDto, RegistrationDto>(request.SelfUpdateDto);
            registrationDto.UserName = myUserName;
            
            return await Handle(new AdminUpdateMemberCommand(registrationDto), cancellationToken);
        }

        public async Task<Unit> Handle(AdminUpdateMemberCommand request, CancellationToken cancellationToken)
        {
            Members theMember = await _membersRepo.GetByIdAsync(request.RegistrationDto.UserName);
            if (theMember is null)
                throw new CustomStatusException("Member was not found.", 404);
            theMember.FullName = request.RegistrationDto.FullName;
            theMember.BirthDate = request.RegistrationDto.BirthDate;
            theMember.Address = request.RegistrationDto.Address;
            request.RegistrationDto.Password.CreatePasswordHash(out string hashedPass);
            theMember.Password = hashedPass;

            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            #region user name check
            bool doesExist = await DoesEntityExistAsync(request.RegistrationDto.UserName);
            if (doesExist)
                throw new CustomBusinessException($"This user name ({request.RegistrationDto.UserName}) has been already taken.");
            #endregion
            Members newMember = _mapper.Map<RegistrationDto, Members>(request.RegistrationDto);
            newMember.Password.CreatePasswordHash(out string hashedPass);
            newMember.Password = hashedPass;

            var roles = _rolesRepo.GetLocalViewWithLinqExp(x => _defaultMemberRoleNames.Contains(x.RoleName));
            newMember.Roles = new List<Roles>();

            newMember.Roles.AddRange(roles);

            await _membersRepo.InsertAsync(newMember);

            await _unitOfWork.CommitAsync();

            return newMember.UserName;
        }

        #region private methods
        public async Task<bool> DoesEntityExistAsync(string Id)
        {
            return await _membersRepo.GetDataWithLinqExp(x => x.UserName == Id).AnyAsync();
        }
        #endregion
    }
}
