using DishDash.BLL.Interfaces;
using DishDash.Models.DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace DishDash.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerReadDto>>> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerReadDto>> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerReadDto>> Create(
        CustomerCreateDto customerDto)
    {
        var customer = await _customerService.CreateCustomerAsync(customerDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = customer.CustomerId },
            customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    CustomerCreateDto customerDto)
    {
        var updated = await _customerService.UpdateCustomerAsync(
            id,
            customerDto);

        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}