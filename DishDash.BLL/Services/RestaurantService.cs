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
public class RestaurantService : IRestaurantService
{
    private readonly DishDashDbContext _context;

    public RestaurantService(DishDashDbContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantReadDto>> GetAllRestaurantsAsync()
    {
        return await _context.Restaurants
            .Select(r => new RestaurantReadDto
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                Address = r.Address,
                PhoneNumber = r.PhoneNumber
            })
            .ToListAsync();
    }

    public async Task<RestaurantReadDto?> GetRestaurantByIdAsync(int id)
    {
        return await _context.Restaurants
            .Where(r => r.RestaurantId == id)
            .Select(r => new RestaurantReadDto
            {
                RestaurantId = r.RestaurantId,
                Name = r.Name,
                Address = r.Address,
                PhoneNumber = r.PhoneNumber
            })
            .FirstOrDefaultAsync();
    }

    public async Task<RestaurantReadDto> CreateRestaurantAsync(RestaurantCreateDto restaurantDto)
    {
        var restaurant = new Restaurant
        {
            Name = restaurantDto.Name,
            Address = restaurantDto.Address,
            PhoneNumber = restaurantDto.PhoneNumber
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();

        return new RestaurantReadDto
        {
            RestaurantId = restaurant.RestaurantId,
            Name = restaurant.Name,
            Address = restaurant.Address,
            PhoneNumber = restaurant.PhoneNumber
        };
    }

    public async Task<bool> UpdateRestaurantAsync(
        int id,
        RestaurantCreateDto restaurantDto)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return false;
        }

        restaurant.Name = restaurantDto.Name;
        restaurant.Address = restaurantDto.Address;
        restaurant.PhoneNumber = restaurantDto.PhoneNumber;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return false;
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();

        return true;
    }
}
