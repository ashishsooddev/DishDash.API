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
}
