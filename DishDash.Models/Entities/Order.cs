using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishDash.Models.Entities;

public class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; } = null!;

}