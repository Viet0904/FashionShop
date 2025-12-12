
using FashionShop.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Infrastructure.Persistence
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly FashionShopDbContext _db;

        public EfRepository(FashionShopDbContext db)
        {
            _db = db;
        }

        public async Task<T?> GetByIdAsync(object id, CancellationToken ct = default)
        {
            return await _db.Set<T>().FindAsync(new[] { id }, ct);
        }

        public async Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default)
        {
            return await _db.Set<T>().ToListAsync(ct);
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _db.Set<T>().AddAsync(entity, ct);
        }

        public Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _db.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity, CancellationToken ct = default)
        {
            _db.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<T> Query()
        {
            return _db.Set<T>().AsQueryable();
        }
    }
}
