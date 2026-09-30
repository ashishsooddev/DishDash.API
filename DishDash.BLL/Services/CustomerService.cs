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
public class CustomerService : ICustomerService
{
    private readonly DishDashDbContext _context;
    public CustomerService(DishDashDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerReadDto>> GetAllCustomersAsync(
        int pageNumber,
        int pageSize)
    {
        return await _context.Customers
            .Select(c => new CustomerReadDto
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<CustomerReadDto?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .Where(c => c.CustomerId == id)
            .Select(c => new CustomerReadDto
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerDto)
    {
        var customer = new Customer
        {
            FirstName = customerDto.FirstName,
            LastName = customerDto.LastName,
            Email = customerDto.Email,
            PhoneNumber = customerDto.PhoneNumber
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new CustomerReadDto
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        };
    }
    public async Task<bool> UpdateCustomerAsync(int id, CustomerCreateDto customerDto)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return false;
        }

        customer.FirstName = customerDto.FirstName;
        customer.LastName = customerDto.LastName;
        customer.Email = customerDto.Email;
        customer.PhoneNumber = customerDto.PhoneNumber;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return false;
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }
}