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
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Customers.Add(new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" });
                context.Customers.Add(new CustomerDto { Id = 2, FirstName = "Jane", LastName = "Smith" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CustomerRepository(context);

                // Act
                var customers = repository.GetCustomers();

                // Assert
                Assert.Equal(2, customers.Count());
            }
        }

        [Fact]
        public void AddCustomer_AddsCustomerToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var repository = new CustomerRepository(context);
                var customer = new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" };

                // Act
                repository.AddCustomer(customer);
                repository.SaveChanges();
            }

            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal("John", context.Customers.First().FirstName);
            }
        }

        [Fact]
        public void GetCustomer_ReturnsCustomerById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Customers.Add(new CustomerDto { Id = 1, FirstName = "John", LastName = "Doe" });
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

