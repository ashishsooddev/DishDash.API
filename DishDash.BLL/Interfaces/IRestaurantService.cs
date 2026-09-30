using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishDash.Models.DTOs;

namespace DishDash.BLL.Interfaces;
public interface IRestaurantService
{
    Task<List<RestaurantReadDto>> GetAllRestaurantsAsync();
    Task<RestaurantReadDto?> GetRestaurantByIdAsync(int id);
    Task<RestaurantReadDto> CreateRestaurantAsync(RestaurantCreateDto restaurantDto);

    Task<bool> UpdateRestaurantAsync(int id, RestaurantCreateDto restaurantDto);

    Task<bool> DeleteRestaurantAsync(int id);
}
