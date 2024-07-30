using System;
using Domain.Models;

namespace Domain.RepositoriyInterfaces
{
	public interface IAddressRepository
	{
        Task<IEnumerable<Address>> GetAddressesByCustomerId(int customerId);
        Task<Address> GetAddressById(int addressId);
        Task UpdateAddress(Address address);
        Task AddAddress(int customerId, Address address);
        Task DeleteAddress(int addressId);

        Task<int> GetCountryIdByName(string countryName);

    }
}

