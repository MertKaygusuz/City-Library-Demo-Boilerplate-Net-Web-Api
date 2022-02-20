using CityLibraryInfrastructure.DtoBase;
using CityLibraryInfrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sorts by ISortingDto rules
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortingList"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, List<SortingDto> sortingList)
        {
            if (sortingList is null)
                return source;
            var expression = source.Expression;
            byte count = 0;
            foreach (var item in sortingList)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.SortingPropertyName);
                var method = item.SortingDirection == SortingDirections.Descending ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }
    }
}
