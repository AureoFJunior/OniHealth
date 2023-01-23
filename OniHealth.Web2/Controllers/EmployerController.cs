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
    public class EmployerController : Controller
    {
        private readonly IEmployerService<Employer> _employerService;
        private readonly IRepository<Employer> _employerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public EmployerController(IRepository<Employer> employerRepository,
            IEmployerService<Employer> employerService,
            IMapper mapper,
            IValidator validator)
        {
            _employerService = employerService;
            _employerRepository = employerRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the employers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployers()
        {
            IEnumerable<Employer> employers = await _employerRepository.GetAllAsync();
            if (employers == null)
                _validator.AsNotFound("Employees not found.");

            IEnumerable<EmployerDTO> employer = _mapper.Map<IEnumerable<EmployerDTO>>(employers);
            return Ok(employer);
        }

        /// <summary>
        /// Get a employer.
        /// </summary>
        /// <param name="id">Id of the employer</param>
        /// <returns>The employer that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployer(int id)
        {
            Employer employer = await _employerRepository.GetByIdAsync(id);
            if (employer == null)
                _validator.AsNotFound("Employee not found.");

            EmployerDTO employerDTO = _mapper.Map<EmployerDTO>(employer);
            return Ok(employerDTO);
        }

        /// <summary>
        /// Add a new employer
        /// </summary>
        /// <param name="employerDTO">Employer's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployer([FromBody] EmployerDTO employerDTO)
        {
            Employer employer = _mapper.Map<Employer>(employerDTO);
            Employer createdEmployer = await _employerService.CreateAsync(employer);
            employerDTO = _mapper.Map<EmployerDTO>(createdEmployer);
            return Ok(employerDTO);
        }

        /// <summary>
        /// Update an employer
        /// </summary>
        /// <param name="employerDTO">Employer's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployer([FromBody] EmployerDTO employerDTO)
        {
            Employer employer = _mapper.Map<Employer>(employerDTO);
            Employer updatedEmployer = _employerService.Update(employer);
            if (updatedEmployer == null)
            _validator.AsNotFound("Employee not found.");

            employerDTO = _mapper.Map<EmployerDTO>(updatedEmployer);
            return Ok(employerDTO);
        }

        /// <summary>
        /// Delete an employer
        /// </summary>
        /// <param name="id">Employer's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            Employer employer = _employerService.Delete(id);
            if (employer == null)
            _validator.AsNotFound("Employee not found.");

            EmployerDTO employerDTO = _mapper.Map<EmployerDTO>(employer);
            return Ok(employerDTO);
        }
    }
}