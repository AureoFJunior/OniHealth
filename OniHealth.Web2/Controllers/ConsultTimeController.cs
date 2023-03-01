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
using System;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ConsultTimeController : Controller
    {
        private readonly IConsultTimeService<ConsultTime> _consultTimeService;
        private readonly IRepository<ConsultTime> _consultTimeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public ConsultTimeController(
            IConsultTimeService<ConsultTime> consultTimeService,
            IRepository<ConsultTime> consultTimeRepository,
            IMapper mapper,
            IValidator validator)
        {
            _consultTimeRepository = consultTimeRepository;
            _consultTimeService = consultTimeService;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the consults times.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetConsultsTime()
        {
            IEnumerable<ConsultTime> consultsTime = await _consultTimeRepository.GetAllAsync();
            if (consultsTime == null)
            {
                _validator.AsNotFound("Consults times not found.");
                return NotFound();
            }

            IEnumerable<ConsultTimeDTO> consultsTimeDTO = _mapper.Map<IEnumerable<ConsultTimeDTO>>(consultsTime);
            return Ok(consultsTimeDTO);
        }

        /// <summary>
        /// Get a consult time.
        /// </summary>
        /// <param name="id">Id of the consult time</param>
        /// <returns>The consult time that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultTime(int id)
        {
            ConsultTime consultTime = await _consultTimeRepository.GetByIdAsync(id);
            if (consultTime == null)
            {
                _validator.AsNotFound("Consult time not found.");
                return NotFound();
            }

            ConsultTimeDTO consultTimeDTO = _mapper.Map<ConsultTimeDTO>(consultTime);
            return Ok(consultTimeDTO);
        }

        /// <summary>
        /// Update an consult time
        /// </summary>
        /// <param name="consultTimeDTO">Consult's times to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateConsultTime([FromBody] ConsultTimeDTO consultTimeDTO)
        {
            ConsultTime consultTime = _mapper.Map<ConsultTime>(consultTimeDTO);
            ConsultTime updatedConsultTime = _consultTimeService.Update(consultTime);
            if (updatedConsultTime == null)
            {
                _validator.AsNotFound("Consult time not found.");
                return NotFound();
            }

            consultTimeDTO = _mapper.Map<ConsultTimeDTO>(updatedConsultTime);
            return Ok(consultTimeDTO);
        }

        /// <summary>
        /// Delete an consult time
        /// </summary>
        /// <param name="id">Consult's ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteConsultTime(int id)
        {
            ConsultTime consultTime = _consultTimeService.Delete(id);
            if (consultTime == null)
            {
                _validator.AsNotFound("Consult time not found.");
                return NotFound();
            }

            ConsultTimeDTO consultTimeDTO = _mapper.Map<ConsultTimeDTO>(consultTime);
            return Ok(consultTimeDTO);
        }
    }
}