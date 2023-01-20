using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;

namespace OniHealth.Domain.Models
{
    public class CustomerService : ICustomerService<Customer>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository =customerRepository;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer existentCustomer = _customerRepository.GetById(customer.Id);
            Customer includedCustomer = new Customer();

            if (existentCustomer == null)
            {
                includedCustomer = await _customerRepository.CreateAsync(customer);
                return includedCustomer;
            }

            throw new InsertDatabaseException();
        }

        public Customer Update(Customer customer)
        {
            Customer existentCustomer = _customerRepository.GetById(customer.Id);
            Customer updatedCustomer = new Customer();

            if (customer != null)
            { 
                updatedCustomer = _customerRepository.Update(customer);
                return updatedCustomer;
            }
            else
                return null;
        }

        public Customer Delete(int id)
        {
            Customer customer = _customerRepository.GetById(id);
            Customer deletedCustomer = new Customer();

            if (customer != null)
            {
                deletedCustomer = _customerRepository.Delete(customer);
                return deletedCustomer;
            }
            else
                return null;
        }
    }
}