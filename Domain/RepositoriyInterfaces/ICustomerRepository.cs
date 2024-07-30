using System;
using Domain.Models;

namespace Domain.RepositoriyInterfaces
{
	public interface ICustomerRepository
	{
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerId);
    }
}

