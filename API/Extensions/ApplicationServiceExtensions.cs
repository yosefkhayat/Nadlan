using Application;
using Application.Core;
using Application.Interfaces;
using Application.Listings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Photos;
using Infrastructure.Security;
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

            //add FluentValidation to services
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Create>();

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            return services;
        }

    }
}
