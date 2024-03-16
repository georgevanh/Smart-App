using System.Collections.Generic;
using Moq;
using SmartExercise.Server.Models;
using SmartExercise.Server.Repositories;
using SmartExercise.Server.Services;
using Xunit;

namespace SmartExerciseServerTest.Services
{
    public class CustomerServiceTests
    {
        [Fact]
        public void GetCustomers_ReturnsListOfCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new CustomerDto { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomers()).Returns(customers);

            var service = new CustomerService(mockRepository.Object);

            // Act
            var result = service.GetCustomers();

            // Assert
            Assert.Equal(customers, result);
        }

        [Fact]
        public void AddCustomer_AddsCustomer()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" };

            var mockRepository = new Mock<ICustomerRepository>();
            var service = new CustomerService(mockRepository.Object);

            // Act
            service.AddCustomer(customer);

            // Assert
            mockRepository.Verify(repo => repo.AddCustomer(customer), Times.Once);
            mockRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetCustomer_ReturnsCustomerById()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" };

            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetCustomer(1)).Returns(customer);

            var service = new CustomerService(mockRepository.Object);

            // Act
            var result = service.GetCustomer(1);

            // Assert
            Assert.Equal(customer, result);
        }
    }
}
