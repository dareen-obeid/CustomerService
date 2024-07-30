using System;
using Application.DTOs;
using Application.Services.Interfaces;
using Application.Validation;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoriyInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddressDto> _addressValidator;

        public AddressService(IAddressRepository addressRepository, IMapper mapper, IValidator<AddressDto> addressValidator)
		{
            _addressRepository = addressRepository;
            _mapper = mapper;
            _addressValidator = addressValidator;

        }


        public async Task<AddressDto> GetAddressById(int addressId)
        {
            var address = await _addressRepository.GetAddressById(addressId);
            if (address == null || !address.IsActive)
            {
                throw new NotFoundException($"Address with ID {addressId} not found or is inactive.");
            }
            return _mapper.Map<AddressDto>(address);        }

        public async Task<IEnumerable<AddressDto>> GetAddressesByCustomerId(int customerId)
        {
            var addresses = await _addressRepository.GetAddressesByCustomerId(customerId);
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }



        public async Task UpdateAddress(int addressId, AddressDto addressDto)
        {
            _addressValidator.Validate(addressDto);
            int countryId = await _addressRepository.GetCountryIdByName(addressDto.CountryName);

            var address = await _addressRepository.GetAddressById(addressId);
            if (address == null || !address.IsActive)
            {
                throw new NotFoundException($"Address with ID {addressId} not found or is inactive.");
            }

            _mapper.Map(addressDto, address);
            address.CountryId = countryId;
            await _addressRepository.UpdateAddress(address);
        }

        public async Task AddAddress(int customerId, AddressDto addressDto)
        {
            _addressValidator.Validate(addressDto);
            int countryId = await _addressRepository.GetCountryIdByName(addressDto.CountryName);

            var address = _mapper.Map<Address>(addressDto);
            address.CountryId = countryId;
            address.CustomerId = customerId;

            await _addressRepository.AddAddress(customerId,address);
        }

        public async Task DeleteAddress(int addressId)
        {
            var address = await _addressRepository.GetAddressById(addressId);
            if (address == null)
            {
                throw new NotFoundException($"Address with ID {addressId} not found.");
            }
            await _addressRepository.DeleteAddress(addressId);
        }
    }
}

