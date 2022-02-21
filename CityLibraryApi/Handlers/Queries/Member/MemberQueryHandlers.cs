using CityLibraryApi.Queries.Member;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CityLibraryApi.Handlers.Queries.Member
{
    public class MemberQueryHandlers : IRequestHandler<GetMemberByUserNameQuery, Members>
    {
        private readonly IMembersRepo _membersRepo;
        public MemberQueryHandlers(IMembersRepo membersRepo)
        {
            _membersRepo = membersRepo;
        }
        public async Task<Members> Handle(GetMemberByUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _membersRepo.GetDataWithLinqExp(x => x.UserName == request.UserName, "Roles")
                                     .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
