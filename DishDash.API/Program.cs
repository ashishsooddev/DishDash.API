using DishDash.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DishDash.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //Services registered 

            // Add OpenAPI support
            builder.Services.AddOpenApi();

            // Add database connection -- my connection string added.
            builder.Services.AddDbContext<DishDashDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DishDashConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Generate the OpenAPI document
                app.MapOpenApi();

                // Display the Swagger UI
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "DishDash API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}