using System;
using System.ComponentModel.DataAnnotations;
using Application.DTOs;

namespace Application.Validation
{
    public class AddressValidator : IValidator<AddressDto>
    {
        public void Validate(AddressDto address)
        {
            if (string.IsNullOrWhiteSpace(address.City) || address.City.Length > 50)
                throw new ValidationException("City name must be between 1 and 50 characters.");

            if (!string.IsNullOrEmpty(address.State) && address.State.Length > 50)
                throw new ValidationException("State name must be up to 50 characters if provided.");

            if (string.IsNullOrWhiteSpace(address.AddressLine) || address.AddressLine.Length > 200)
                throw new ValidationException("Address line must be between 1 and 200 characters.");

            if (string.IsNullOrWhiteSpace(address.PostalCode) || address.PostalCode.Length > 10)
                throw new ValidationException("Postal code must be between 1 and 10 characters.");

            if (address.CreateDate == default)
                throw new ValidationException("Creation date is required.");

            if (address.LastUpdateDate == default)
                throw new ValidationException("Last updated date is required.");

            if (string.IsNullOrWhiteSpace(address.CountryName))
                throw new ValidationException("Country name is required.");
        }
    }

}

