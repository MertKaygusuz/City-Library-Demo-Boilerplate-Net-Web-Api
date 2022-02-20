using System.Collections.Generic;
using System.Linq;

namespace CityLibraryInfrastructure.MapperConfigurations
{
    public interface ICustomMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : class where TSource : class;

        TDestination MapToExistingObject<TSource, TDestination>(TSource source, TDestination destination) where TDestination : class where TSource : class;

        IEnumerable<TDestination> MapAsNumarable<TSource, TDestination>(IQueryable<TSource> source) where TDestination : class where TSource : class;

        IQueryable<TDestination> MapAsQueryable<TSource, TDestination>(IQueryable<TSource> source) where TDestination : class where TSource : class;
    }
}
