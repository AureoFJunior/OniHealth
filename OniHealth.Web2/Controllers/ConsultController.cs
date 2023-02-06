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
    public class ConsultController : Controller
    {
        private readonly IConsultService<Consult> _consultService;
        private readonly IConsultTimeService<ConsultTime> _consultTimeService;
        private readonly IConsultTypeService<ConsultType> _consultTypeService;
        private readonly IRepositoryConsult _consultRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public ConsultController(IRepositoryConsult consultRepository,
            IConsultService<Consult> consultService,
            IConsultTimeService<ConsultTime> consultTimeService,
            IConsultTypeService<ConsultType> consultTypeService,
            IMapper mapper,
            IValidator validator)
        {
            _consultService = consultService;
            _consultRepository = consultRepository;
            _consultTimeService = consultTimeService;
            _consultTypeService = consultTypeService;
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

        #region Add Consult Methods
        /// <summary>
        /// Add a new consult
        /// </summary>
        /// <param name="consultInsertDTO">Consult's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConsult([FromBody] ConsultInsertDTO consultInsertDTO)
        {
            string queueName = "addConsultQueue";

            ConsultTime consultTime = await AddConsultTime();
            ConsultType consultType = await AddConsultType(consultInsertDTO);

            Consult consult = ConvertToConsult(consultInsertDTO, consultTime, consultType);
            await SharedFunctions.EnqueueAsync(consult, queueName);
            await _consultService.CreateAsync(queueName);
            return Ok();
        }

        private Consult ConvertToConsult(ConsultInsertDTO consultInsertDTO, ConsultTime consultTime, ConsultType consultType)
        {
            consultInsertDTO.Consult.ConsultTimeId = consultTime.Id;
            consultInsertDTO.Consult.ConsultTypeId = consultType.Id;

            Consult consult = _mapper.Map<Consult>(consultInsertDTO.Consult);
            return consult;
        }

        private async Task<ConsultType> AddConsultType(ConsultInsertDTO consultInsertDTO)
        {
            string queueName = "addConsultTypeQueue";
            ConsultType consultType = _mapper.Map<ConsultType>(consultInsertDTO.ConsultType);
            await SharedFunctions.EnqueueAsync(consultType, queueName);
            return await _consultTypeService.CreateAsync(queueName);
        }

        private async Task<ConsultTime> AddConsultTime()
        {
            string queueName = "addConsultTimeQueue";
            await SharedFunctions.EnqueueAsync(new ConsultTime() { AppointmentTime = DateTime.Now }, queueName);
            return await _consultTimeService.CreateAsync(queueName);
        }

        #endregion

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