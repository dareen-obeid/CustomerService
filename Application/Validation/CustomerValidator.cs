using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Application.DTOs;

namespace Application.Validation
{
    public class CustomerValidator : IValidator<CustomerDto>
    {
        private readonly HashSet<string> _validGenders = new HashSet<string> { "Male", "Female", "Other" };

        public void Validate(CustomerDto customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) || customer.FirstName.Length > 50)
                throw new ValidationException("First name must be between 1 and 50 characters.");

            if (string.IsNullOrWhiteSpace(customer.LastName) || customer.LastName.Length > 50)
                throw new ValidationException("Last name must be between 1 and 50 characters.");

            if (!string.IsNullOrWhiteSpace(customer.Email) && !new EmailAddressAttribute().IsValid(customer.Email))
                throw new ValidationException("Invalid email format.");

            if (!string.IsNullOrWhiteSpace(customer.Phone) && !new PhoneAttribute().IsValid(customer.Phone))
                throw new ValidationException("Invalid phone number format.");

            if (customer.CreateDate == default)
                throw new ValidationException("Creation date is required.");

            if (customer.LastUpdateDate == default)
                throw new ValidationException("Last updated date is required.");

            if (string.IsNullOrWhiteSpace(customer.GenderName) || !_validGenders.Contains(customer.GenderName))
                throw new ValidationException("Gender must be 'Male', 'Female', or 'Other'.");

            // Additional validation for the addresses included with the customer
            if (customer.Addresses != null)
            {
                foreach (var address in customer.Addresses)
                {
                    new AddressValidator().Validate(address); // Reuse AddressValidator for each address
                }
            }
        }
    }
}
