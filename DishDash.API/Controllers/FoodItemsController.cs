using DishDash.BLL.Interfaces;
using DishDash.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodItemsController : ControllerBase
{
    private readonly IFoodItemService _foodItemService;

    public FoodItemsController(IFoodItemService foodItemService)
    {
        _foodItemService = foodItemService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FoodItemReadDto>>> GetAll()
    {
        var foodItems = await _foodItemService.GetAllFoodItemsAsync();
        return Ok(foodItems);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodItemReadDto>> GetById(int id)
    {
        var foodItem = await _foodItemService.GetFoodItemByIdAsync(id);

        if (foodItem == null)
        {
            return NotFound();
        }

        return Ok(foodItem);
    }

    [HttpPost]
    public async Task<ActionResult<FoodItemReadDto>> Create(
        FoodItemCreateDto foodItemDto)
    {
        var foodItem = await _foodItemService
            .CreateFoodItemAsync(foodItemDto);

        if (foodItem == null)
        {
            return BadRequest("Restaurant does not exist.");
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = foodItem.FoodItemId },
            foodItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        FoodItemCreateDto foodItemDto)
    {
        var updated = await _foodItemService
            .UpdateFoodItemAsync(id, foodItemDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _foodItemService.DeleteFoodItemAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}