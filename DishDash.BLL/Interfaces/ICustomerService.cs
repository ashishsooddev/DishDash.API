using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishDash.Models.DTOs;

namespace DishDash.BLL.Interfaces;
public interface ICustomerService
{
    Task<List<CustomerReadDto>> GetAllCustomersAsync(int pageNumber, int pageSize);

    Task<CustomerReadDto?> GetCustomerByIdAsync(int id);

    Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerDto);

    Task<bool> UpdateCustomerAsync(int id, CustomerCreateDto customerDto);

    Task<bool> DeleteCustomerAsync(int id);
}
