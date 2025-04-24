using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public abstract class GenericRepository<T, TContext> : IRepository<T>
        where T : class, IEntity
        where TContext : DbContext
    {
        protected TContext DbContext;

        protected GenericRepository(TContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<T?> AddAsync(T entity)
        {
            var added = DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var entity = DbContext.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                DbContext.Set<T>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
