using DishDash.BLL.Interfaces;
using DishDash.BLL.Services;
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
            // added this for problem details
            builder.Services.AddProblemDetails();

            //Services registered below this---> 
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();
            builder.Services.AddScoped<IFoodItemService, FoodItemService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            // Add OpenAPI support
            builder.Services.AddOpenApi();

            // Add database connection -- my connection string added.
            builder.Services.AddDbContext<DishDashDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DishDashConnection")));

            var app = builder.Build();
            // Used for exception handling to throw an error if user asking for which is not there...
            app.UseExceptionHandler();

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