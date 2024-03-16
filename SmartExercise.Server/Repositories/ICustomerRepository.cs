using SmartExercise.Server.Models;

namespace SmartExercise.Server.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerDto> GetCustomers();
        void AddCustomer(CustomerDto customerDto);
        CustomerDto? GetCustomer(int id);
        void SaveChanges();
    }
}
