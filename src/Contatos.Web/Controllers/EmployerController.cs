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

        public EmployerController(EmployerService contatoService,
            IRepository<Employer> contatoRepository)
        {
            _employerService = contatoService;
            _employerRepository = contatoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            var employers = _employerRepository.GetAll();

            var employer = employers.Select(c => new EmployerDTO { Id = c.Id, Nome = c.Nome, Email = c.Email });

            if (employer == null)
                return NotFound(new { message = $"Funcionários não encontrados." });

            return Ok(employer);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionario(int id)
        {
            var employer = _employerRepository.GetById(id);
            if (employer == null)
            {
                return NotFound(new { message = $"Funcionario de id={id} não encontrado" });
            }
            return Ok(employer);
        }
    }
}