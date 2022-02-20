using System.Threading.Tasks;

namespace CityLibraryDomain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
