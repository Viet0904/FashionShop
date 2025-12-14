using FashionShop.Blazor.Services.Http;
using FashionShop.Contracts.Catalog;

namespace FashionShop.Blazor.Services.Products
{
    public class ProductApi : IProductApi
    {
        private readonly ApiClient _api;

        public ProductApi(ApiClient api)
        {
            _api = api;
        }

        public async Task<List<ProductDto>> GetAllAsync()
            => await _api.GetAsync<List<ProductDto>>("api/products") ?? [];
    }

}
