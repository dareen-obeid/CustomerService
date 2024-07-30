using System;
using Application.DTOs;
using Domain.Models;

namespace Application.Services.Interfaces
{
	public interface IAddressService
	{
        Task<IEnumerable<AddressDto>> GetAddressesByCustomerId(int customerId);
        Task<AddressDto> GetAddressById(int addressId);
        Task AddAddress(int customerId, AddressDto addressDto);
        Task UpdateAddress(int addressId, AddressDto addressDto);
        Task DeleteAddress(int addressId);
    }
}

