using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;

namespace CityLibraryDomain.Repositories
{
    public class BooksRepo : BaseRepo<Books, int>, IBooksRepo
    {
        public BooksRepo(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
