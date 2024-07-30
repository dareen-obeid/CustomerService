using System;
using Application.DTOs;
using Domain.Models;

namespace Application.Services.Interfaces
{
	public interface ICustomerService
	{
        Task<CustomerDto> GetCustomerById(int customerId);
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task UpdateCustomer(int customerId, CustomerDto customerDto);
        Task CreateCustomer(CustomerDto customerDto);
        Task DeleteCustomer(int customerId);
    }
}

