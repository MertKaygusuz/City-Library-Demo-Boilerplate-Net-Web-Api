using CityLibraryInfrastructure.BaseInterfaces.Pagination;
using CityLibraryInfrastructure.DtoBase;
using System.Collections.Generic;

namespace CityLibraryApi.Dtos.Base
{
    public class PaginateAndSortingDto : IPaginationDto
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public List<SortingDto> SortingModel { get; set; }
    }
}
