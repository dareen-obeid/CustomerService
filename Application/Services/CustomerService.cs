using System;
using Application.DTOs;
using Application.Services.Interfaces;
using Application.Validation;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.RepositoriyInterfaces;

namespace Application.Services
{
	public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _customerValidator;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IValidator<CustomerDto> customerValidator)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerValidator = customerValidator;

        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
            if (customer == null || !customer.IsActive)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found or is inactive.");
            }
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateCustomer(int customerId, CustomerDto customerDto)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
            if (customer == null || !customer.IsActive)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found or is inactive.");
            }
            _customerValidator.Validate(customerDto);
            _mapper.Map(customerDto, customer);
            await _customerRepository.UpdateCustomer(customer);
        }

        public async Task CreateCustomer(CustomerDto customerDto)
        {
            _customerValidator.Validate(customerDto);
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.CreateCustomer(customer);
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _customerRepository.GetCustomerById(customerId);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }
            await _customerRepository.DeleteCustomer(customerId);
        }


    }
}

