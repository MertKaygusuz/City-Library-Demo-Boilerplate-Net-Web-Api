using CityLibraryInfrastructure.BaseInterfaces.Pagination;

namespace CityLibraryApi.Dtos.Base
{
    public abstract class PaginationDto : IPaginationDto
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
