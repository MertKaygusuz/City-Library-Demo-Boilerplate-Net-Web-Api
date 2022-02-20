using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.DbBase
{
    /// <summary>
    /// Use for (if needed) saving many to many relations without EF Core adding context errors.
    /// </summary>
    public interface IManyToManyLoad
    {
        LocalView<Roles> GetLocalViewWithLinqExp(Expression<Func<Roles, bool>> whereClause);
    }
}
