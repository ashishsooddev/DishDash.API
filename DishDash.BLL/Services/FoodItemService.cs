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
public class FoodItemService : IFoodItemService
{
    private readonly DishDashDbContext _context;

    public FoodItemService(DishDashDbContext context)
    {
        _context = context;
    }
    public async Task<List<FoodItemReadDto>> GetAllFoodItemsAsync()
    {
        return await _context.FoodItems
            .Include(f => f.Restaurant)
            .Select(f => new FoodItemReadDto
            {
                FoodItemId = f.FoodItemId,
                Name = f.Name,
                Description = f.Description,
                Price = f.Price,
                IsAvailable = f.IsAvailable,
                RestaurantId = f.RestaurantId,
                RestaurantName = f.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<FoodItemReadDto?> GetFoodItemByIdAsync(int id)
    {
        return await _context.FoodItems
            .Include(f => f.Restaurant)
            .Where(f => f.FoodItemId == id)
            .Select(f => new FoodItemReadDto
            {
                FoodItemId = f.FoodItemId,
                Name = f.Name,
                Description = f.Description,
                Price = f.Price,
                IsAvailable = f.IsAvailable,
                RestaurantId = f.RestaurantId,
                RestaurantName = f.Restaurant.Name
            })
            .FirstOrDefaultAsync();
    }
    public async Task<FoodItemReadDto?> CreateFoodItemAsync(
        FoodItemCreateDto foodItemDto)
    {
        var restaurantExists = await _context.Restaurants
            .AnyAsync(r => r.RestaurantId == foodItemDto.RestaurantId);

        if (!restaurantExists)
        {
            return null;
        }

        var foodItem = new FoodItem
        {
            Name = foodItemDto.Name,
            Description = foodItemDto.Description,
            Price = foodItemDto.Price,
            IsAvailable = foodItemDto.IsAvailable,
            RestaurantId = foodItemDto.RestaurantId
        };

        _context.FoodItems.Add(foodItem);
        await _context.SaveChangesAsync();

        return await GetFoodItemByIdAsync(foodItem.FoodItemId);
    }

    public async Task<bool> UpdateFoodItemAsync(
        int id,
        FoodItemCreateDto foodItemDto)
    {
        var foodItem = await _context.FoodItems.FindAsync(id);

        if (foodItem == null)
        {
            return false;
        }

        var restaurantExists = await _context.Restaurants
            .AnyAsync(r => r.RestaurantId == foodItemDto.RestaurantId);

        if (!restaurantExists)
        {
            return false;
        }

        foodItem.Name = foodItemDto.Name;
        foodItem.Description = foodItemDto.Description;
        foodItem.Price = foodItemDto.Price;
        foodItem.IsAvailable = foodItemDto.IsAvailable;
        foodItem.RestaurantId = foodItemDto.RestaurantId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFoodItemAsync(int id)
    {
        var foodItem = await _context.FoodItems.FindAsync(id);

        if (foodItem == null)
        {
            return false;
        }

        _context.FoodItems.Remove(foodItem);
        await _context.SaveChangesAsync();

        return true;
    }
}
