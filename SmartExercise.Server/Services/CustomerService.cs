using SmartExercise.Server.Models;
using SmartExercise.Server.Repositories;

namespace SmartExercise.Server.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public void AddCustomer(CustomerDto customerDto)
        {
            _customerRepository.AddCustomer(customerDto);
            _customerRepository.SaveChanges();
        }

        public CustomerDto GetCustomer(int id)
        {
            return _customerRepository.GetCustomer(id);
        }
    }

}
