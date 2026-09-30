using DishDash.BLL.Interfaces;
using DishDash.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RestaurantReadDto>>> GetAll()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantReadDto>> GetById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);

        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantReadDto>> Create(
        RestaurantCreateDto restaurantDto)
    {
        var restaurant = await _restaurantService
            .CreateRestaurantAsync(restaurantDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = restaurant.RestaurantId },
            restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        RestaurantCreateDto restaurantDto)
    {
        var updated = await _restaurantService
            .UpdateRestaurantAsync(id, restaurantDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _restaurantService.DeleteRestaurantAsync(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}