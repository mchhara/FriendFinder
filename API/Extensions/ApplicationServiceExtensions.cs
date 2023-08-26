using API.Data;
using API.Services;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.SignlaR;
using API.Data.Migrations;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}