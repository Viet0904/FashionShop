using System.Net.Http.Json;

namespace FashionShop.Blazor.Services.Http
{
    public class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<T?> GetAsync<T>(string url)
            => await _http.GetFromJsonAsync<T>(url);

        public async Task PostAsync<T>(string url, T data)
            => await _http.PostAsJsonAsync(url, data);
    }

}
