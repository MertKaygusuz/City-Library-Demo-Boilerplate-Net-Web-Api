using CityLibraryInfrastructure.DbBase;
using CityLibraryInfrastructure.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;

namespace CityLibraryInfrastructure.Repositories
{
    public interface IRolesRepo : IBaseRepo<Roles, int>, IManyToManyLoad
    {
        
    }
}
