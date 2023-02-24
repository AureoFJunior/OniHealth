using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;
using OniHealth.Web.Config;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class PlansController : Controller
    {
        private readonly IPlanService<Plans> _planService;
        private readonly IRepositoryPlans _planRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public PlansController(IPlanService<Plans> planService, IRepositoryPlans planRepository, IMapper mapper, IValidator validator)
        {
            _planService = planService;
            _planRepository = planRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the plans.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            IEnumerable<Plans> plan = await _planRepository.GetAllAsync();
            if (plan == null)
            {
                _validator.AsNotFound("Plan not found.");
                return NotFound();
            }

            IEnumerable<PlansDTO> planDTO = _mapper.Map<IEnumerable<PlansDTO>>(plan);
            return Ok(planDTO);
        }

        /// <summary>
        /// Get a plan.
        /// </summary>
        /// <param name="id">Id of the plan</param>
        /// <returns>The plan that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlan(int id)
        {
            Plans plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
            {
                _validator.AsNotFound("Plan not found.");
                return NotFound();
            }

            PlansDTO planDTO = _mapper.Map<PlansDTO>(plan);
            return Ok(planDTO);
        }

        /// <summary>
        /// Get the plan name.
        /// </summary>
        /// <param name="id">Id of the plans</param>
        /// <returns>The plan name that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanName(int id)
        {
            string planName = await _planRepository.GetNameByIdAsync(id);
            return Ok(planName);
        }

        /// <summary>
        /// Get the plan name by customer.
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <returns>The plan that match with the customerId parameter.</returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetPlanNameByCustomer(int customerId)
        {
            string planName = await _planRepository.GetNameByCustomerAsync(customerId);
            return Ok(planName);
        }

        /// <summary>
        /// Add a new plan
        /// </summary>
        /// <param name="planDTO">Plan to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPlan([FromBody] PlansDTO planDTO)
        {
            Plans plan = _mapper.Map<Plans>(planDTO);
            Plans createdplan = await _planService.CreateAsync(plan);
            planDTO = _mapper.Map<PlansDTO>(createdplan);
            return Ok(planDTO);
        }

        /// <summary>
        /// Update an plan
        /// </summary>
        /// <param name="planDTO">Plan to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePlan([FromBody] PlansDTO planDTO)
        {
            Plans plan = _mapper.Map<Plans>(planDTO);
            Plans updatedPlan = _planService.Update(plan);
            if (updatedPlan == null)
            {
                _validator.AsNotFound("Plan not found.");
                return NotFound();
            }

            planDTO = _mapper.Map<PlansDTO>(updatedPlan);
            return Ok(planDTO);
        }

        /// <summary>
        /// Delete an plan
        /// </summary>
        /// <param name="id">Plan Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePlan(int id)
        {
            Plans plan = _planService.Delete(id);
            if (plan == null)
            {
                _validator.AsNotFound("Plan not found.");
                return NotFound();
            }

            PlansDTO planDTO = _mapper.Map<PlansDTO>(plan);
            return Ok(planDTO);
        }

    }
}
