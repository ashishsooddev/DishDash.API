using DishDash.BLL.Interfaces;
using DishDash.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderReadDto>>> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReadDto>> GetById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderReadDto>> Create(
        OrderCreateDto orderDto)
    {
        var order = await _orderService.CreateOrderAsync(orderDto);

        if (order == null)
        {
            return BadRequest(
                "Customer, restaurant, or food item information is invalid.");
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = order.OrderId },
            order);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] string status)
    {
        var updated = await _orderService
            .UpdateOrderStatusAsync(id, status);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _orderService.DeleteOrderAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();

    }
}