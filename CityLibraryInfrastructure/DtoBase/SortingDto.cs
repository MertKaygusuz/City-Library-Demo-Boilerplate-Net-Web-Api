using CityLibraryInfrastructure.BaseInterfaces.Pagination;
using CityLibraryInfrastructure.Enums;

namespace CityLibraryInfrastructure.DtoBase
{
    public class SortingDto : ISortingDto
    {
        public string SortingPropertyName { get; set; }

        public SortingDirections SortingDirection { get; set; }
    }
}
