using API.Extensions;
using API.Middleware;
using Application;
using Application.Core;
using Application.Listings;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using API.SignalR;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            // Add services to the container.

            builder.Services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            //Create databse if not exist
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    context.Database.Migrate();
                    Seed.SeedData(context, userManager,roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured dueing migration");
                }
            }

            app.UseDefaultFiles();
            app.UseStaticFiles(); 

            // Configure the HTTP request pipeline.

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.MapControllers();
            app.MapHub<ChatHub>("/chat");
            app.MapFallbackToController("Index", "Fallback");

            app.Run();
        }
    }
}