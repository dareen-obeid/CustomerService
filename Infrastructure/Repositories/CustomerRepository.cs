using System;
using System.Net;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoriyInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers
                            .Include(c => c.Addresses.Where(a => a.IsActive))
                                .ThenInclude(a => a.Country)
                            .Include(c => c.Gender)
                            .FirstOrDefaultAsync(c => c.CustomerId == customerId && c.IsActive);

            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                  .Where(c => c.IsActive)
                  .Include(c => c.Gender)
                  .Include(c => c.Addresses.Where(a => a.IsActive))
                    .ThenInclude(a => a.Country)
                  .ToListAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            customer.LastUpdateDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers
                        .Include(c => c.Addresses)
                        .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new NotFoundException("customer not found.");
            }

            customer.IsActive = false;
            foreach (var address in customer.Addresses)
            {
                address.IsActive = false;
            }

            customer.LastUpdateDate = DateTime.UtcNow;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}

