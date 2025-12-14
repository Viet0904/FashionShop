using FashionShop.Blazor.Services;
using FashionShop.Blazor.Services.Http;
using FashionShop.Blazor.Services.Products;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace FashionShop.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HttpClient config => trỏ tới API 
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

            // Register Api Clients
            builder.Services.AddScoped<ProductApiClient>();
            builder.Services.AddScoped<ApiClient>();
            builder.Services.AddScoped<IProductApi, ProductApi>();

            await builder.Build().RunAsync();
        }
    }
}
