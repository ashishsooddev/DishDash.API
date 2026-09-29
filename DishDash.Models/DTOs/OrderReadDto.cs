using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.DTOs;

public class OrderReadDto
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = string.Empty;

    public List<OrderItemReadDto> OrderItems { get; set; } = new();
}
