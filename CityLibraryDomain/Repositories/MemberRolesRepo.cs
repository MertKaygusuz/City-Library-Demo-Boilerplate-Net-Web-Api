using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;

namespace CityLibraryDomain.Repositories
{
    public class MemberRolesRepo : BaseRepo<MemberRoles, int>, IMemberRolesRepo
    {
        public MemberRolesRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
