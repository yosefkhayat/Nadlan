using Application;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    /// <summary>
    /// This class is house keeping for the main program.
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //add seviece to coonction to the sqlite database
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(connectionString);
            });
            //add seviece to CorsPolicy th the client side
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });
            //add Mediator to services
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(List.Query)));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            return services;
        }

    }
}
