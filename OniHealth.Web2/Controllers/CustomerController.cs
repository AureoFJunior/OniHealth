using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AutoMapper;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Web.Config;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService<Customer> _customerService;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public CustomerController(IRepository<Customer> customerRepository,
            ICustomerService<Customer> customerService,
            IMapper mapper,
            IValidator validator)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllAsync();
            if (customers == null)
            {
                _validator.AsNotFound("Customers not found.");
                return NotFound();
            }

            IEnumerable<CustomerDTO> customer = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(customer);
        }

        /// <summary>
        /// Get a customer.
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>The customer that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                _validator.AsNotFound("Customer not found.");
                return NotFound();
            }

            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="CustomerDTO">Customer to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        {
            Customer customer = _mapper.Map<Customer>(customerDTO);
            Customer createdCustomer = await _customerService.CreateAsync(customer);
            customerDTO = _mapper.Map<CustomerDTO>(createdCustomer);
            return Ok(customerDTO);
        }

        /// <summary>
        /// Update an customer
        /// </summary>
        /// <param name="customerDTO">Customer's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO customerDTO)
        {
            Customer customer = _mapper.Map<Customer>(customerDTO);
            Customer updatedCustomer = _customerService.Update(customer);
            if (updatedCustomer == null)
            {
                _validator.AsNotFound("Customer not found.");
                return NotFound();
            }

            customerDTO = _mapper.Map<CustomerDTO>(updatedCustomer);
            return Ok(customerDTO);
        }

        /// <summary>
        /// Delete an customer
        /// </summary>
        /// <param name="id">Customer's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer customer = _customerService.Delete(id);
            if (customer == null)
            {
                _validator.AsNotFound("Customer not found.");
                return NotFound();
            }

            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }
    }
}