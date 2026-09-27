using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public int FoodItemId { get; set; }

    public FoodItem FoodItem { get; set; } = null!;
}
