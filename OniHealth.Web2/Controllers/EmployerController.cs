using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Web.DTOs;
using System.Threading.Tasks;
using System;

namespace OniHealth.Web.Controllers
{
    [Route("api/[controller]")]
    public class EmployerController : Controller
    {
        private readonly EmployerService _employerService;
        private readonly IRepository<Employer> _employerRepository;

        public EmployerController(EmployerService employerService,
            IRepository<Employer> employerRepository)
        {
            _employerService = employerService;
            _employerRepository = employerRepository;
        }

        /// <summary>
        /// Return all the employers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployers()
        {
            try
            {
                IEnumerable<Employer> employers = _employerRepository.GetAll();

                IEnumerable<EmployerDTO> employer = employers.Where(x => x != null).Select(x => new EmployerDTO { Id = x.Id, Name = x.Name, Email = x.Email, Role = x.Role });

                if (!employer.Any())
                    return NotFound(new { message = $"Funcionários não encontrados." });

                return Ok(employer);

            }catch(Exception ex) { return Problem($"Erro ao buscar registros de Funcionários: {ex.Message}"); }
        }

        /// <summary>
        /// Get a employer.
        /// </summary>
        /// <param name="id">Id of the employer</param>
        /// <returns>The employer that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployer(int id)
        {
            try
            {
                Employer employer = _employerRepository.GetById(id);
                if (employer == null)
                {
                    return NotFound(new { message = $"Funcionario de id={id} não encontrado" });
                }
                return Ok(employer);
            }
            catch (Exception ex) { return Problem($"Erro ao buscar registro de Funcionário: {ex.Message}"); }
        }

        /// <summary>
        /// Add a new employer
        /// </summary>
        /// <param name="name">Employer's name</param>
        /// <param name="email">Employer's email</param>
        /// <param name="role">Employer's role</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployer(string name, string email, EmployerRole role)
        {
            try
            {
                Employer employer = await _employerService.CreateAsync(0, name, email, role);

                return Ok(employer);

            } catch (Exception ex){ return Problem($"Erro ao criar registro de Funcionário: {ex.Message}");}
        }

        /// <summary>
        /// Update an employer
        /// </summary>
        /// <param name="name">Employer's name</param>
        /// <param name="email">Employer's email</param>
        /// <param name="role">Employer's role</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployer(int id, string name, string email, EmployerRole role)
        {
            try
            {
                Employer employer = _employerService.Update(id, name, email, role);
                return Ok(employer);

            }
            catch (Exception ex) { return Problem($"Erro ao atualizar registro de Funcionário: {ex.Message}"); }
        }

        /// <summary>
        /// Delete an employer
        /// </summary>
        /// <param name="name">Employer's name</param>
        /// <param name="email">Employer's email</param>
        /// <param name="role">Employer's role</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            try
            {
                Employer employer = _employerService.Delete(id);
                return Ok(employer);

            }
            catch (Exception ex) { return Problem($"Erro ao remover registro de Funcionário: {ex.Message}"); }
        }
    }
}