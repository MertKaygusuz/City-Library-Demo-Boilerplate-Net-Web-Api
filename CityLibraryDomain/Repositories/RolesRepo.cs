using CityLibraryDomain.ContextRelated;
using CityLibraryDomain.DbBase.Repositories.Base;
using CityLibraryInfrastructure.DbBase;
using CityLibraryInfrastructure.Entities;
using CityLibraryInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;

namespace CityLibraryDomain.Repositories
{
    public class RolesRepo : BaseRepo<Roles, int>, IRolesRepo
    {
        public RolesRepo(AppDbContext dbContext) : base(dbContext)
        {

        }

        public LocalView<Roles> GetLocalViewWithLinqExp(Expression<Func<Roles, bool>> whereClause)
        {
            var roles = GetDataWithLinqExp(whereClause);
            _dbcontext.Roles.AttachRange(roles);
            return _dbcontext.Roles.Local;
        }
    }
}
