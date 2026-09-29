using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.DTOs;
public class FoodItemReadDto
{
    public int FoodItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = string.Empty;
}