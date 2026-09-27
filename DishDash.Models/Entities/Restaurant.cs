using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.Entities;

public class Restaurant
{
    public int RestaurantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

}