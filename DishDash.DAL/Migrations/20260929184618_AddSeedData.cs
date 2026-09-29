using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DishDash.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.smith@example.com", "John", "Smith", "204-555-1001" },
                    { 2, "sarah.johnson@example.com", "Sarah", "Johnson", "204-555-1002" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "Tasty Bites", "204-555-2001" },
                    { 2, "456 Portage Avenue", "Pizza House", "204-555-2002" }
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "IsAvailable", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Grilled chicken burger with lettuce and sauce", true, "Chicken Burger", 12.99m, 1 },
                    { 2, "Crispy golden French fries", true, "French Fries", 5.99m, 1 },
                    { 3, "Large pizza with pepperoni and cheese", true, "Pepperoni Pizza", 18.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "RestaurantId", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pending", 18.98m },
                    { 2, 2, new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Completed", 18.99m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "FoodItemId", "OrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 12.99m },
                    { 2, 2, 1, 1, 5.99m },
                    { 3, 3, 2, 1, 18.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 2);
        }
    }
}
