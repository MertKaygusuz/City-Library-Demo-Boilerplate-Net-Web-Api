using CityLibraryInfrastructure.DbBase;
using CityLibraryInfrastructure.Entities;

namespace CityLibraryInfrastructure.Repositories
{
    public interface IBooksRepo : IBaseRepo<Books, int>
    {
    }
}
