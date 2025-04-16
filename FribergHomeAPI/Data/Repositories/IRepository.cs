using System.Linq.Expressions;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IRepository<T>
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(int id);
        Task<T?> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(int id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T entity);
    }
}