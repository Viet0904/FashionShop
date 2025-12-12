using AutoMapper;
using FashionShop.Api.Mapping;
using FashionShop.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FashionShop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            // Add services to the container.

            builder.Services.AddControllers();

            // Register Infrastructure (DbContext, Repositories, Services)
            builder.Services.AddInfrastructure(builder.Configuration);

            // CORS (allow Blazor client later on 2 ports if needed)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalBlazor", policy =>
                {
                    policy.WithOrigins("https://localhost:5003", "http://localhost:5003") // adjust Blazor URL if different
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var jwtCfg = builder.Configuration.GetSection("Jwt");
            var key = jwtCfg.GetValue<string>("Key");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtCfg.GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = jwtCfg.GetValue<string>("Audience"),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateLifetime = true
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });


            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FashionShop API", Version = "v1" });
            });

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FashionShop API V1");
                    c.RoutePrefix = string.Empty; // Swagger at root
                });
            }
            

            app.UseHttpsRedirection();

            app.UseCors("AllowLocalBlazor");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
