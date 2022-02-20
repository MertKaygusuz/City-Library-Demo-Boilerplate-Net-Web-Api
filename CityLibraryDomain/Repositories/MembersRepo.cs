using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;

namespace CityLibraryDomain.Repositories
{
    public class MembersRepo : BaseRepo<Members, string>, IMembersRepo
    {
        public MembersRepo(AppDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
