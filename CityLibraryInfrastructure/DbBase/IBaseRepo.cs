﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.DbBase
{
    public interface IBaseRepo<T, S> where T : TableBase where S : IConvertible
    {
        Task InsertAsync(T entity);
        Task InsertManyAsyc(IEnumerable<T> entities);
        void Update(T entity);
        T DeleteById(S Id);

        Task<T> DeleteByIdAsync(S Id);

        T Delete(T objectToBeDeleted);

        void DeleteMany(IEnumerable<T> objectsToBeDeleted);

        IQueryable<T> GetData(bool forWrite = false);

        IQueryable<T> GetDataWithLinqExp(Expression<Func<T, bool>> whereClause, bool forWrite = false);

        IQueryable<T> GetDataWithLinqExp(Expression<Func<T, bool>> whereClause, params string[] navObjects);

        Task<T> GetByIdAsync(S Id);

        Task<bool> DoesEntityExistAsync(S Id);

        Task<int> CountAsync(Expression<Func<T, bool>> whereClause = null);

        int Count(Expression<Func<T, bool>> whereClause = null);

        Task<long> LongCountAsync(Expression<Func<T, bool>> whereClause = null);

        long LongCount(Expression<Func<T, bool>> whereClause = null);

        T GetById(S Id);

        Task SaveChangesAsync();

        void SaveChanges();

    }
}
