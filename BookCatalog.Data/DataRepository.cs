using BookCatalog.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Data
{
    public class DataRepository(DbContext db) : IDataRepository
    {
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await db.Set<T>().ToListAsync();
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync<T>(int id) where T : class
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity is null)
                throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {id} not found.");
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }
        public async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            return await db.Set<T>().FindAsync(id);
        }
        public Task UpdateAsync<T>(T entity) where T : class
        {
            db.Set<T>().Update(entity);
            return db.SaveChangesAsync();
        }
    }
}