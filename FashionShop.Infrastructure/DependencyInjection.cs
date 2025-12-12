using FashionShop.Application.Catalog;
using FashionShop.Application.Common;
using FashionShop.Infrastructure.Catalog;
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

            services.AddScoped<Application.Auth.IAuthService, Infrastructure.Auth.AuthService>();

            // Generic repository + UnitOfWork
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
