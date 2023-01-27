using System.Collections.Generic;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Web.Config;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OniHealth.Domain.Utils;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ConsultController : Controller
    {
        private readonly IConsultService<Consult> _consultService;
        private readonly IRepositoryConsult _consultRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public ConsultController(IRepositoryConsult consultRepository,
            IConsultService<Consult> consultService,
            IMapper mapper,
            IValidator validator)
        {
            _consultService = consultService;
            _consultRepository = consultRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the consults.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetConsults()
        {
            IEnumerable<Consult> consults = await _consultRepository.GetAllAsync();
            if (consults == null)
            {
                _validator.AsNotFound("Consults not found.");
                return NotFound();
            }

            IEnumerable<ConsultDTO> consult = _mapper.Map<IEnumerable<ConsultDTO>>(consults);
            return Ok(consult);
        }

        /// <summary>
        /// Get a consult.
        /// </summary>
        /// <param name="id">Id of the consult</param>
        /// <returns>The consult that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsult(int id)
        {
            Consult consult = await _consultRepository.GetByIdAsync(id);
            if (consult == null)
            {
                _validator.AsNotFound("Consult not found.");
                return NotFound();
            }

            ConsultDTO consultDTO = _mapper.Map<ConsultDTO>(consult);
            return Ok(consultDTO);
        }

        /// <summary>
        /// Add a new consult
        /// </summary>
        /// <param name="consultDTO">Consult's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConsult([FromBody] ConsultDTO consultDTO)
        {
            string queueName = "addConsultQueue";
            Consult consult = _mapper.Map<Consult>(consultDTO);
            await SharedFunctions.EnqueueAsync(consult, queueName);
            Consult createdConsult = new Consult();
            await SharedFunctions.DequeueAndProcessAsync<Consult>(queueName);
            consultDTO = _mapper.Map<ConsultDTO>(createdConsult);
            return Ok(consultDTO);
        }

        /// <summary>
        /// Update an consult
        /// </summary>
        /// <param name="consultDTO">Consult's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateConsult([FromBody] ConsultDTO consultDTO)
        {
            Consult consult = _mapper.Map<Consult>(consultDTO);
            Consult updatedConsult = _consultService.Update(consult);
            if (updatedConsult == null)
            {
                _validator.AsNotFound("Consult not found.");
                return NotFound();
            }

            consultDTO = _mapper.Map<ConsultDTO>(updatedConsult);
            return Ok(consultDTO);
        }

        /// <summary>
        /// Delete an consult
        /// </summary>
        /// <param name="id">Consult's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteConsult(int id)
        {
            Consult consult = _consultService.Delete(id);
            if (consult == null)
            {
                _validator.AsNotFound("Consult not found.");
                return NotFound();
            }

            ConsultDTO consultDTO = _mapper.Map<ConsultDTO>(consult);
            return Ok(consultDTO);
        }
    }
}