using CityLibraryDomain.ContextRelated;
using CityLibraryInfrastructure.DbBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryDomain.DbBase.Repositories.Base
{
    public class BaseRepo<T, S> : IBaseRepo<T, S> where T : TableBase where S : IConvertible
    {
        protected readonly AppDbContext _dbcontext;

        public BaseRepo(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async virtual Task InsertAsync(T entity)
        {
            await _dbcontext.AddAsync(entity);
        }

        public async virtual Task InsertManyAsyc(IEnumerable<T> entities)
        {
            await _dbcontext.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            _dbcontext.Update(entity);
        }

        public virtual T DeleteById(S id)
        {
            var set = _dbcontext.Set<T>();
            return set.Remove(set.Find(id)!).Entity;
        }

        public virtual void DeleteMany(IEnumerable<T> objectsToBeDeleted)
        {
            _dbcontext.Set<T>().RemoveRange(objectsToBeDeleted);
        }

        public virtual async Task<T> DeleteByIdAsync(S id)
        {
            var set = _dbcontext.Set<T>();
            return set.Remove((await set.FindAsync(id))!).Entity;
        }

        public virtual T Delete(T objectToBeDeleted)
        {
            return _dbcontext.Remove<T>(objectToBeDeleted).Entity;
        }

        public virtual IQueryable<T> GetData(bool forWrite = false)
        {
            if (forWrite)
                return _dbcontext.Set<T>()/*.Where(f => f.DeletedAt == null)*/;
            else
                return _dbcontext.Set<T>().AsNoTracking()/*.Where(f => f.DeletedAt == null)*/;
        }


        public virtual IQueryable<T> GetDataWithLinqExp(Expression<Func<T, bool>> whereClause, bool forWrite = false)
        {
            return GetData(forWrite).Where(whereClause);
        }

        public virtual IQueryable<T> GetDataWithLinqExp(Expression<Func<T, bool>> whereClause, params string[] navObjects)
        {
            var query = GetData(false).Where(whereClause);
            foreach (var navObject in navObjects)
            {
                query = query.Include(navObject);
            }

            return query;
        }

        public async virtual Task<T> GetByIdAsync(S id)
        {
            return await _dbcontext.FindAsync<T>(id);
        }

        public async virtual Task<bool> DoesEntityExistAsync(S id)
        {
            return await GetByIdAsync(id) is not null;
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereClause = null)
        {
            if (whereClause is null)
                return await _dbcontext.Set<T>().CountAsync();

            return await _dbcontext.Set<T>().CountAsync(whereClause);
        }

        public virtual int Count(Expression<Func<T, bool>> whereClause = null)
        {
            if (whereClause is null)
                return _dbcontext.Set<T>().Count();

            return _dbcontext.Set<T>().Count(whereClause);
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<T, bool>> whereClause = null)
        {
            if (whereClause is null)
                return await _dbcontext.Set<T>().LongCountAsync();

            return await _dbcontext.Set<T>().LongCountAsync(whereClause);
        }

        public virtual long LongCount(Expression<Func<T, bool>> whereClause = null)
        {
            if (whereClause is null)
                return _dbcontext.Set<T>().LongCount();

            return _dbcontext.Set<T>().LongCount(whereClause);
        }

        public virtual T GetById(S id)
        {
            return _dbcontext.Find<T>(id);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public virtual void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
