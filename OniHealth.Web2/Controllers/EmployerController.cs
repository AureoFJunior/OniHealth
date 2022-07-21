using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Web.DTOs;
using System.Threading.Tasks;

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
            IEnumerable<Employer> employers = _employerRepository.GetAll();

            IEnumerable<EmployerDTO> employer = employers.Where(x => x != null).Select(x => new EmployerDTO { Id = x.Id, Name = x.Name, Email = x.Email, Role = x.Role });

            if (!employer.Any())
                return NotFound(new { message = $"Funcionários não encontrados." });

            return Ok(employer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployer(int id)
        {
            Employer employer = _employerRepository.GetById(id);
            if (employer == null)
            {
                return NotFound(new { message = $"Funcionario de id={id} não encontrado" });
            }
            return Ok(employer);
        }
    }
}