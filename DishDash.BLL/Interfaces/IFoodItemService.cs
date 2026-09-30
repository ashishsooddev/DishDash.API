using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishDash.Models.DTOs;

namespace DishDash.BLL.Interfaces;
public interface IFoodItemService
{
    Task<List<FoodItemReadDto>> GetAllFoodItemsAsync();

    Task<FoodItemReadDto?> GetFoodItemByIdAsync(int id);

    Task<FoodItemReadDto?> CreateFoodItemAsync(FoodItemCreateDto foodItemDto);

    Task<bool> UpdateFoodItemAsync(int id, FoodItemCreateDto foodItemDto);

    Task<bool> DeleteFoodItemAsync(int id);
}