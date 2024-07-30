using System;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoriyInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAddressesByCustomerId(int customerId)
        {
            return await _context.Addresses
                  .Where(a => a.CustomerId == customerId && a.IsActive)
                  .Include(a => a.Country)
                  .ToListAsync();
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            var address = await _context.Addresses
                .Include(a => a.Country)
                .FirstOrDefaultAsync(a => a.AddressId == addressId && a.IsActive);

            if (address == null)
            {
                throw new NotFoundException($"Address with ID {addressId} not found.");
            }

            return address;
        }


        public async Task UpdateAddress(Address address)
        {
            _context.Entry(address).State = EntityState.Modified;
            address.LastUpdateDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task AddAddress(int customerId, Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAddress(int addressId)
        {
            var address = await _context.Addresses.FindAsync(addressId);

            if (address == null)
            {
                throw new NotFoundException("Address not found.");
            }

            address.IsActive = false;
            address.LastUpdateDate = DateTime.UtcNow;

            _context.Entry(address).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountryIdByName(string countryName)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.CountryName == countryName);
            if (country == null)
            {
                throw new NotFoundException("Country not found.");
            }
            return country.CountryId;
        }

    }
}

