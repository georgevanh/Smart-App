using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartExercise.Server.Data;
using SmartExercise.Server.Models;
using SmartExercise.Server.Repositories;
using Xunit;

namespace SmartExerciseServerTest.Repositories
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void GetCustomers_ReturnsListOfCustomers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Customers.Add(new CustomerDto { Id = 0, FirstName = "John", LastName = "Doe", Address = "1 John s", Email = "Doe@gmail.com", MobileNumber = "0412345678"});
                context.Customers.Add(new CustomerDto { Id = 0, FirstName = "Jane", LastName = "Smith", Address = "1 John s", Email = "Doe@gmail.com", MobileNumber = "0412345678" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CustomerRepository(context);

                // Act
                var customers = repository.GetCustomers();

                // Assert
                Assert.NotEqual(0, customers.Count());
            }
        }

        [Fact]
        public void AddCustomer_AddsCustomerToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = new CustomerDto { Id = 0, FirstName = "John", LastName = "Doe", Address = "1 John s", Email = "Doe@gmail.com", MobileNumber = "0412345678" };

                // Act
                repository.AddCustomer(customer);
                repository.SaveChanges();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal("John", context.Customers.First().FirstName);
            }
        }

        [Fact]
        public void GetCustomer_ReturnsCustomerById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Customers.Add(new CustomerDto { Id = 0, FirstName = "John", LastName = "Doe", Address = "1 John s", Email = "Doe@gmail.com", MobileNumber = "0412345678" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CustomerRepository(context);

                // Act
                var customer = repository.GetCustomer(1);

                // Assert
                Assert.NotNull(customer);
                Assert.Equal("John", customer.FirstName);
            }
        }

        
    }
}

