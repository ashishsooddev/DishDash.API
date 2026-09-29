using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DishDash.Models.DTOs;

public class OrderItemCreateDto
{
    [Range(1, 100)]
    public int Quantity { get; set; }

    [Range(1, int.MaxValue)]
    public int FoodItemId { get; set; }
}