using FashionShop.Application.Catalog;
using FashionShop.Application.Common;
using FashionShop.Domain.Catalog;
using FashionShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Infrastructure.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
        {
            // Dùng Query() để có thể Include nếu muốn
            var query = _productRepository
                .Query()
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .AsNoTracking();

            return await query.ToListAsync(ct);
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var query = _productRepository
                .Query()
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken ct = default)
        {
            await _productRepository.AddAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return product;
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            await _productRepository.UpdateAsync(product, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _productRepository.GetByIdAsync(id, ct);
            if (existing == null) throw new KeyNotFoundException("Product not found");
            await _productRepository.RemoveAsync(existing, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }

    }
}
