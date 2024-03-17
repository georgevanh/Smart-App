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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Get all customers from the database.
        /// </summary>
        /// <returns>An enumerable collection of customer DTOs.</returns>
        public IEnumerable<CustomerDto> GetCustomers()
        {
            try
            {
                return _dbContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                // Handle specific exceptions here or log the error.
                throw new Exception("Failed to get customers from the database.", ex);
            }
        }

        /// <summary>
        /// Add a new customer to the database.
        /// </summary>
        /// <param name="customerDto">The customer DTO to add.</param>
        public void AddCustomer(CustomerDto customerDto)
        {
            try
            {
                _dbContext.Customers.Add(customerDto);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception.
                throw new Exception("Failed to add customer to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions.
                throw new Exception("An error occurred while adding customer.", ex);
            }
        }

        /// <summary>
        /// Get a customer by ID from the database.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>The customer DTO if found, null otherwise.</returns>
        public CustomerDto? GetCustomer(int id)
        {
            try
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
                return customer != null ? MapToDto(customer) : null;
            }
            catch (Exception ex)
            {
                // Handle specific exceptions here or log the error.
                throw new Exception($"Failed to get customer with ID {id} from the database.", ex);
            }
        }

        /// <summary>
        /// Save changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception.
                throw new Exception("Failed to save changes to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions.
                throw new Exception("An error occurred while saving changes.", ex);
            }
        }

        /// <summary>
        /// Map customer entity to customer DTO.
        /// </summary>
        /// <param name="customer">The customer entity to map.</param>
        /// <returns>The mapped customer DTO.</returns>
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
