using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Infra.Context;
using OniHealth.Domain.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly IRepository<Customer> _repositoryCustomer;

        public CustomerController(CustomerService customerService, IRepository<Customer> repositoryCustomer)
        {
            _customerService = customerService;
            _repositoryCustomer = repositoryCustomer;
        }

        /// <summary>
        /// Return all the customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customers = await _repositoryCustomer.GetAllAsync();

                IEnumerable<CustomerDTO> customer = customers.Where(x => x != null)
                      .Select(x => new CustomerDTO { Id = x.Id, Name = x.Name, Email = x.Email, BirthDate = x.BirthDate, SignedPlan = x.SignedPlan, IsDependent = x.IsDependent, PhoneNumber = x.PhoneNumber });

                if (!customer.Any())
                {
                    return NotFound(new { message = $"Customers not found." });
                }

                return Ok(customer);
            }

            catch (Exception e)
            {
                return Problem($"Error at customers search: {e.Message}");
            }
        }

        /// <summary>
        /// Get a customer.
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>The customer that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                Customer customer = await _repositoryCustomer.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound(new { message = $"Customer with the ID={id} was not found" });
                }

                return Ok(customer);
            }
            catch (Exception e)
            {
                return Problem($"Error at customer search: {e.Message}");
            }
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customer">Customer to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            try
            {
                Customer createdCustomer = await _customerService.CreateAsync(customer);

                return Ok(createdCustomer);
            }
            catch (Exception e)
            {
                return Problem($"Error at customer creation: {e.Message}");
            }
        }

        /// <summary>
        /// Update an customer
        /// </summary>
        /// <param name="customer">Customer to be updated.</param>
        /// <returns></returns>
        [HttpPut] 
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                Customer updatedCustomer =  _customerService.Update(customer);

                return Ok(updatedCustomer);
            }
            catch (Exception e)
            {
                return Problem($"Error at customer update: {e.Message}");
            }
        }

        /// <summary>
        /// Delete an customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                Customer deletedCustomer = _customerService.Delete(id);

                return Ok(deletedCustomer);

            }
            catch (Exception e)
            {
                return Problem($"Error at delete customer: {e.Message}");
            }
        }
    }
}
