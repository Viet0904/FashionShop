using FashionShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FashionShopDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Sau này sẽ AddScoped repository, service, v.v. ở đây
            // vd:
            // services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
