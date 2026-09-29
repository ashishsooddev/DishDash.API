using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DishDash.Models.DTOs;
public class OrderCreateDto
{
    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }

    [Range(1, int.MaxValue)]
    public int RestaurantId { get; set; }

    [Required]
    [MinLength(1)]
    public List<OrderItemCreateDto> OrderItems { get; set; } = new();
}