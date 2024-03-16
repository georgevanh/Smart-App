using SmartExercise.Server.Models;

namespace SmartExercise.Server.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetCustomers();
        void AddCustomer(CustomerDto customerDto);
        CustomerDto GetCustomer(int id);
    }
}
