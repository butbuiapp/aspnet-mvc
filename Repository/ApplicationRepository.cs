
using Microsoft.EntityFrameworkCore;
using PurchaseManagement.Entity;

namespace PurchaseManagement.Repository
{
    public class ApplicationRepository<T> : IApplicationRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;

        public ApplicationRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = this._dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            this._dbSet.Add(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public T Create(T entity)
        {
            this._dbSet.Add(entity);
            this._dbContext.SaveChanges();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            this._dbSet.Remove(entity);
            await this._dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public List<T> GetAll()
        {
            return this._dbSet.ToList();
        }

        public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await this._dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
            } else
            {
                return await this._dbSet.Where(predicate).FirstOrDefaultAsync();
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this._dbSet.Update(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
