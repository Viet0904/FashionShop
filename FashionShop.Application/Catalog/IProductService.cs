using FashionShop.Domain.Catalog;


namespace FashionShop.Application.Catalog
{
    public interface IProductService
    {
        // Lấy toàn bộ sản phẩm (sau này mình sẽ nâng cấp có filter/paging)
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);

        // Lấy 1 sản phẩm theo Id (dùng cho trang chi tiết, admin edit...)
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);

        // Admin operations
        Task<Product> CreateAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
