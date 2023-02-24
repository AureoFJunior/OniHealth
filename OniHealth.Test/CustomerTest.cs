using OniHealth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OniHealth.Infra.Repositories;
using System.Xml.Serialization;
using Xunit;
using OniHealth.Domain.Enums;

namespace OniHealth.Test
{
    public class CustomerTest
    {
        private readonly CustomerService _customerService;
        private readonly CustomerRepository _customerRepository;

        public CustomerTest()
        {
            _customerRepository = new CustomerRepository(new ContextFactory().Context);
            _customerService = new CustomerService(_customerRepository);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            int customerId = await _customerRepository.GetLastId();

            Assert.NotNull(await _customerRepository.GetByIdAsync(customerId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _customerRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Customer customer = new Customer("Customer", "customerTeste@gmail.com", new DateTime(2002, 1, 18).ToUniversalTime(), 9, true, "999999999", new DateTime(2006, 6, 25).ToUniversalTime());
            Assert.NotNull(await _customerService.CreateAsync(customer));
        }

        [Fact]
        public async void Update()
        {
            int customerId = await _customerRepository.GetLastId();

            Customer customer = _customerRepository.GetById(customerId);
            Assert.NotNull(_customerService.Update(customer));
        }

        [Fact]
        public async void Delete()
        {
            int customerId = await _customerRepository.GetLastId();
            Assert.NotNull(_customerService?.Delete(customerId));
        }
    }
}
