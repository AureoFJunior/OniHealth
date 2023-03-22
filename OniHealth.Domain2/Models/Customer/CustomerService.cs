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
                await _customerRepository.CommitAsync();
                return includedCustomer;
            }

            throw new InsertDatabaseException();
        }

        public Customer Update(Customer customer)
        {
            Customer existentCustomer = _customerRepository.GetById(customer.Id);
            Customer updatedCustomer = new Customer();

            if (existentCustomer != null)
            { 
                updatedCustomer = _customerRepository.Update(customer);
                _customerRepository.Commit();
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
                _customerRepository.Commit();
                return deletedCustomer;
            }
            else
                return null;
        }
    }
}