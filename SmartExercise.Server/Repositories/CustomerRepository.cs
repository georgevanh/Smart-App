using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartExercise.Server.Data;
using SmartExercise.Server.Models;

namespace SmartExercise.Server.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public void AddCustomer(CustomerDto customerDto)
        {
            _dbContext.Customers.Add(customerDto);
        }

        public CustomerDto? GetCustomer(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            return customer != null ? MapToDto(customer) : null;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        private CustomerDto MapToDto(CustomerDto customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                MobileNumber = customer.MobileNumber,
                Address = customer.Address
            };
        }

    }
}
