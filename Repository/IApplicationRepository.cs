using System.Linq.Expressions;

namespace PurchaseManagement.Repository
{
    public interface IApplicationRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool useNoTracking = false);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);

        T Create(T entity);
        List<T> GetAll();
    }
}
