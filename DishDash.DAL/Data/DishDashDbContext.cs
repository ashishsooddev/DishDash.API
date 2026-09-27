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
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.FoodItems)
            .WithOne(f => f.Restaurant)
            .HasForeignKey(f => f.RestaurantId);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<FoodItem>()
            .HasMany(f => f.OrderItems)
            .WithOne(oi => oi.FoodItem)
            .HasForeignKey(oi => oi.FoodItemId);

        modelBuilder.Entity<FoodItem>()
            .Property(f => f.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(10, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(10, 2);
    }
}
