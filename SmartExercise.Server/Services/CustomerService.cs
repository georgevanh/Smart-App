using Microsoft.EntityFrameworkCore;
using SmartExercise.Server.Models;
using SmartExercise.Server.Repositories;
using System;
using System.Collections.Generic;

namespace SmartExercise.Server.Services
{
    /// <summary>
    /// Service for managing customer data.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <returns>An enumerable collection of customer DTOs.</returns>
        public IEnumerable<CustomerDto> GetCustomers()
        {
            try
            {
                return _customerRepository.GetCustomers();
            }
            catch (Exception ex)
            {
                // Handle specific exceptions here or log the error.
                throw new Exception("Failed to get customers.", ex);
            }
        }

        /// <summary>
        /// Add a new customer.
        /// </summary>
        /// <param name="customerDto">The customer DTO to add.</param>
        public void AddCustomer(CustomerDto customerDto)
        {
            try
            {
                _customerRepository.AddCustomer(customerDto);
                _customerRepository.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception.
                throw new Exception("Failed to add customer.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions.
                throw new Exception("An error occurred while adding customer.", ex);
            }
        }

        /// <summary>
        /// Get a customer by ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer DTO if found, null otherwise.</returns>
        public CustomerDto GetCustomer(int id)
        {
            try
            {
                return _customerRepository.GetCustomer(id);
            }
            catch (Exception ex)
            {
                // Handle specific exceptions here or log the error.
                throw new Exception($"Failed to get customer with ID {id}.", ex);
            }
        }
    }
}
