using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DishDash.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DishDash.DAL.Data;

public class DishDashDbContext : DbContext
{
    public DishDashDbContext(DbContextOptions<DishDashDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<FoodItem> FoodItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.FoodItems)
            .WithOne(f => f.Restaurant)
            .HasForeignKey(f => f.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FoodItem>()
            .HasMany(f => f.OrderItems)
            .WithOne(oi => oi.FoodItem)
            .HasForeignKey(oi => oi.FoodItemId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FoodItem>()
            .Property(f => f.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                PhoneNumber = "204-555-1001"
            },
            new Customer
            {
                CustomerId = 2,
                FirstName = "Sarah",
                LastName = "Johnson",
                Email = "sarah.johnson@example.com",
                PhoneNumber = "204-555-1002"
            });

        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant
            {
                RestaurantId = 1,
                Name = "Tasty Bites",
                Address = "123 Main Street",
                PhoneNumber = "204-555-2001"
            },
            new Restaurant
            {
                RestaurantId = 2,
                Name = "Pizza House",
                Address = "456 Portage Avenue",
                PhoneNumber = "204-555-2002"
            });

        modelBuilder.Entity<FoodItem>().HasData(
            new FoodItem
            {
                FoodItemId = 1,
                Name = "Chicken Burger",
                Description = "Grilled chicken burger with lettuce and sauce",
                Price = 12.99m,
                IsAvailable = true,
                RestaurantId = 1
            },
            new FoodItem
            {
                FoodItemId = 2,
                Name = "French Fries",
                Description = "Crispy golden French fries",
                Price = 5.99m,
                IsAvailable = true,
                RestaurantId = 1
            },
            new FoodItem
            {
                FoodItemId = 3,
                Name = "Pepperoni Pizza",
                Description = "Large pizza with pepperoni and cheese",
                Price = 18.99m,
                IsAvailable = true,
                RestaurantId = 2
            });
    }
}
