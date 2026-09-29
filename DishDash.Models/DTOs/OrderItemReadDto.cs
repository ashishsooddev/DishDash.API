using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.DTOs;
public class OrderItemReadDto
{
    public int OrderItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int FoodItemId { get; set; }

    public string FoodItemName { get; set; } = string.Empty;
}
