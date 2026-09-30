using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishDash.Models.DTOs;

namespace DishDash.BLL.Interfaces;
public interface IOrderService
{
    Task<List<OrderReadDto>> GetAllOrdersAsync();

    Task<OrderReadDto?> GetOrderByIdAsync(int id);

    Task<OrderReadDto?> CreateOrderAsync(OrderCreateDto orderDto);
    // task added for complex order query to 
    Task<List<OrderReadDto>> SearchOrdersAsync(string? status, decimal? minimumAmount);

    Task<bool> UpdateOrderStatusAsync(int id, string status);

    Task<bool> DeleteOrderAsync(int id);
}