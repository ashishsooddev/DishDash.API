using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishDash.BLL.Interfaces;
using DishDash.DAL.Data;
using DishDash.Models.DTOs;
using DishDash.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DishDash.BLL.Services;
public class OrderService : IOrderService
{
    private readonly DishDashDbContext _context;

    public OrderService(DishDashDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderReadDto>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Restaurant)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.FoodItem)
            .Select(o => new OrderReadDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                RestaurantId = o.RestaurantId,
                RestaurantName = o.Restaurant.Name,
                OrderItems = o.OrderItems.Select(oi => new OrderItemReadDto
                {
                    OrderItemId = oi.OrderItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    FoodItemId = oi.FoodItemId,
                    FoodItemName = oi.FoodItem.Name
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<OrderReadDto?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Restaurant)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.FoodItem)
            .Where(o => o.OrderId == id)
            .Select(o => new OrderReadDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                RestaurantId = o.RestaurantId,
                RestaurantName = o.Restaurant.Name,
                OrderItems = o.OrderItems.Select(oi => new OrderItemReadDto
                {
                    OrderItemId = oi.OrderItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    FoodItemId = oi.FoodItemId,
                    FoodItemName = oi.FoodItem.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }
    // Another query added to search orderss
    public async Task<List<OrderReadDto>> SearchOrdersAsync(string? status, decimal? minimumAmount)
    {
        var query = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Restaurant)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.FoodItem)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(o => o.Status == status);
        }

        if (minimumAmount.HasValue)
        {
            query = query.Where(o => o.TotalAmount >= minimumAmount.Value);
        }

        return await query
            .Select(o => new OrderReadDto
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                RestaurantId = o.RestaurantId,
                RestaurantName = o.Restaurant.Name,
                OrderItems = o.OrderItems.Select(oi => new OrderItemReadDto
                {
                    OrderItemId = oi.OrderItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    FoodItemId = oi.FoodItemId,
                    FoodItemName = oi.FoodItem.Name
                }).ToList()
            })
            .ToListAsync();
    }
    public async Task<OrderReadDto?> CreateOrderAsync(OrderCreateDto orderDto)
    {
        var customerExists = await _context.Customers
            .AnyAsync(c => c.CustomerId == orderDto.CustomerId);

        if (!customerExists)
        {
            return null;
        }

        var restaurantExists = await _context.Restaurants
            .AnyAsync(r => r.RestaurantId == orderDto.RestaurantId);

        if (!restaurantExists)
        {
            return null;
        }

        var foodItemIds = orderDto.OrderItems
            .Select(item => item.FoodItemId)
            .ToList();

        var foodItems = await _context.FoodItems
            .Where(f => foodItemIds.Contains(f.FoodItemId))
            .ToListAsync();

        if (foodItems.Count != foodItemIds.Count)
        {
            return null;
        }

        foreach (var item in foodItems)
        {
            if (item.RestaurantId != orderDto.RestaurantId)
            {
                return null;
            }

            if (!item.IsAvailable)
            {
                return null;
            }
        }

        var order = new Order
        {
            OrderDate = DateTime.Now,
            Status = "Pending",
            CustomerId = orderDto.CustomerId,
            RestaurantId = orderDto.RestaurantId
        };

        decimal totalAmount = 0;

        foreach (var itemDto in orderDto.OrderItems)
        {
            var foodItem = foodItems
                .First(f => f.FoodItemId == itemDto.FoodItemId);
            var orderItem = new OrderItem
            {
                Quantity = itemDto.Quantity,
                UnitPrice = foodItem.Price,
                FoodItemId = foodItem.FoodItemId
            };

            totalAmount += foodItem.Price * itemDto.Quantity;
            order.OrderItems.Add(orderItem);
        }

        order.TotalAmount = totalAmount;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(order.OrderId);
    }

    public async Task<bool> UpdateOrderStatusAsync(int id, string status)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return false;
        }
        order.Status = status;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
        {
            return false;
        }

        _context.OrderItems.RemoveRange(order.OrderItems);
        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        return true;
    }
}