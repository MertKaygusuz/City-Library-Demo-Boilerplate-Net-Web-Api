using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;

namespace CityLibraryDomain.Repositories
{
    public class ActiveBookReservationsRepo : BaseRepo<ActiveBookReservations, int>, IActiveBookReservationsRepo
    {
        public ActiveBookReservationsRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
