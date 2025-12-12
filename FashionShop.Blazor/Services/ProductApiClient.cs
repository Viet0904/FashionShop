using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CatalogProduct = FashionShop.Contracts.Catalog.ProductDto;

namespace FashionShop.Blazor.Services
{
    public class ProductApiClient
    {
        private readonly HttpClient _http;

        public ProductApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CatalogProduct>?> GetAllAsync(CancellationToken ct = default)
        {
            var resp = await _http.GetFromJsonAsync<List<CatalogProduct>>("api/Products", ct);
            return resp;
        }

        public async Task<CatalogProduct?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _http.GetFromJsonAsync<CatalogProduct>($"api/Products/{id}", ct);
    }
}
