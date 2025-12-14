namespace FashionShop.Blazor.Services.Products
{
    public interface IProductApi
    {
        Task<List<ProductDto>> GetAllAsync();
    }

}
