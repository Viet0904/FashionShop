
namespace FashionShop.Application.Common
{
    public interface IRepository<T> where T : class
    {
        // Lấy 1 entity theo Id
        Task<T?> GetByIdAsync(object id, CancellationToken ct = default);

        // Lấy toàn bộ (dùng cho đơn giản, sau này sẽ thêm filter/paging)
        Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);

        // Thêm mới
        Task AddAsync(T entity, CancellationToken ct = default);

        // Cập nhật
        Task UpdateAsync(T entity, CancellationToken ct = default);

        // Xóa
        Task RemoveAsync(T entity, CancellationToken ct = default);

        // Trả về IQueryable để service bên dưới có thể build query (Where, Include, ...)
        IQueryable<T> Query();
    }
}
