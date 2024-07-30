using System;
using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAddressesByCustomerId(int customerId)
        {
            var addresses = await _addressService.GetAddressesByCustomerId(customerId);
            return Ok(addresses);
        }

        [HttpGet("GetAddress/{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var address = await _addressService.GetAddressById(addressId);
            return Ok(address);
        }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> AddAddress(int customerId, [FromBody] AddressDto addressDto)
        {
            try
            {
                await _addressService.AddAddress(customerId, addressDto);
                return CreatedAtAction(nameof(GetAddress), new { addressId = addressDto.AddressId }, addressDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressDto addressDto)
        {
            await _addressService.UpdateAddress(addressId, addressDto);
            return NoContent();
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            await _addressService.DeleteAddress(addressId);
            return NoContent();
        }
    }

}