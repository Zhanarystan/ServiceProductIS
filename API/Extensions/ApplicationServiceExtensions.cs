using API.Core;
using API.Data;
using API.Interfaces;
using API.Repositories;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
             IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("DatabaseConnection"));
            });

            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000");
                });
            });

            //Repository register
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Service register
            services.AddScoped<IEstablishmentService, EstablishmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            //services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            services.AddSignalR();        
            return services;
        }
    }
}