using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;

namespace CityLibraryDomain.Repositories
{
    public class BookReservationHistoriesRepo : BaseRepo<BookReservationHistories, int>, IBookReservationHistoriesRepo
    {
        public BookReservationHistoriesRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
