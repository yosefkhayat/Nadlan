using API.Extensions;
using Application;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);
            var app = builder.Build();


            //Create databse if not exist
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    Seed.SeedData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured dueing migration");
                }
            }

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.MapControllers();

            app.Run();
        }
    }
}