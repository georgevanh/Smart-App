using System;
using Xunit;
using SmartExercise.Server.Controllers;
using SmartExercise.Server.Services;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SmartExercise.Server.Models;
using System.Collections.Generic;

namespace SmartExerciseServerTest.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _customerController;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _customerController = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public void GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto> { new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" } };
            _mockCustomerService.Setup(repo => repo.GetCustomers()).Returns(customers);

            // Act
            var result = _customerController.GetCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(okResult.Value);
            Assert.Equal(customers, model);
        }

        [Fact]
        public void GetCustomers_ReturnsNotFound_WhenNoCustomersExist()
        {
            // Arrange
            _mockCustomerService.Setup(repo => repo.GetCustomers()).Returns(new List<CustomerDto>());

            // Act
            var result = _customerController.GetCustomers();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddCustomer_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = "John", LastName = "Doe" };

            // Act
            var result = _customerController.AddCustomer(customerDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockCustomerService.Verify(repo => repo.AddCustomer(customerDto), Times.Once);
        }

        [Fact]
        public void AddCustomer_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _customerController.ModelState.AddModelError("FirstName", "FirstName is required");

            // Act
            var result = _customerController.AddCustomer(new CustomerDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetCustomer_ReturnsOkResult_WithValidId()
        {
            // Arrange
            int id = 1;
            var customer = new CustomerDto { Id = id, FirstName = "John", LastName = "Doe" };
            _mockCustomerService.Setup(repo => repo.GetCustomer(id)).Returns(customer);

            // Act
            var result = _customerController.GetCustomer(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CustomerDto>(okResult.Value);
            Assert.Equal(customer, model);
        }

        [Fact]
        public void GetCustomer_ReturnsNotFound_WithInvalidId()
        {
            // Arrange
            int id = 1;
            _mockCustomerService.Setup(repo => repo.GetCustomer(id)).Returns((CustomerDto)null);

            // Act
            var result = _customerController.GetCustomer(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddCustomer_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = "John", LastName = "Doe" };
            _mockCustomerService.Setup(repo => repo.AddCustomer(customerDto)).Throws(new Exception("An error occurred"));

            // Act
            var result = _customerController.AddCustomer(customerDto);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetCustomers_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            _mockCustomerService.Setup(repo => repo.GetCustomers()).Throws(new Exception("An error occurred"));

            // Act
            var result = _customerController.GetCustomers();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetCustomer_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            // Arrange
            int id = 1;
            _mockCustomerService.Setup(repo => repo.GetCustomer(id)).Throws(new Exception("An error occurred"));

            // Act
            var result = _customerController.GetCustomer(id);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public void AddCustomer_ReturnsInternalServerError_WhenModelStateIsValidButServiceThrowsException()
        {
            // Arrange
            var customerDto = new CustomerDto { FirstName = "John", LastName = "Doe" };
            _customerController.ModelState.AddModelError("FirstName", "FirstName is required");
            _mockCustomerService.Setup(repo => repo.AddCustomer(customerDto)).Throws(new Exception("An error occurred"));

            // Act
            var result = _customerController.AddCustomer(customerDto);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
