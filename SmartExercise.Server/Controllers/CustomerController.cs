using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartExercise.Server.Models;
using SmartExercise.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SmartExercise.Server.Controllers
{
    /// <summary>
    /// API controller for managing customers
    /// </summary>
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Constructor for CustomerController
        /// </summary>
        /// <param name="customerService">The customer service</param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// Get a list of all customers
        /// </summary>
        /// <returns>The list of customers</returns>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _customerService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customerDto">The customer data</param>
        /// <returns>ActionResult indicating success or failure</returns>
        [HttpPost]
        public IActionResult AddCustomer(CustomerDto customerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _customerService.AddCustomer(customerDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a customer by ID
        /// </summary>
        /// <param name="id">The ID of the customer</param>
        /// <returns>The customer details</returns>
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                var customer = _customerService.GetCustomer(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
