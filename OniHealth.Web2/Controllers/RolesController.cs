using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using OniHealth.Infra.Repositories;
using AutoMapper;
using System.Data;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Web.Config;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly IRolesService<Roles> _rolesService;
        private readonly IRepositoryRoles _rolesRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;


        public RolesController(IRolesService<Roles> rolesService,
            IRepositoryRoles rolesRepository,
            IMapper mapper,
            IValidator validator)
        {
            _rolesService = rolesService;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Return all the roless.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            IEnumerable<Roles> roles = await _rolesRepository.GetAllAsync();
            if (roles == null)
            {
                _validator.AddMessage("Role not found.");
                return NotFound();
            }

            IEnumerable<RolesDTO> rolesDTO = _mapper.Map<IEnumerable<RolesDTO>>(roles);
            return Ok(rolesDTO);
        }

        /// <summary>
        /// Get a roles.
        /// </summary>
        /// <param name="id">Id of the roles</param>
        /// <returns>The roles that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            Roles roles = await _rolesRepository.GetByIdAsync(id);
            if (roles == null)
            {
                _validator.AddMessage("Role not found.");
                return NotFound();
            }

            RolesDTO role = _mapper.Map<RolesDTO>(roles);
            return Ok(role);
        }

        /// <summary>
        /// Get the role's name.
        /// </summary>
        /// <param name="id">Id of the roles</param>
        /// <returns>The role's name that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleName(int id)
        {
            string roleName = await _rolesRepository.GetNameByIdAsync(id);
            return Ok(roleName);
        }

        /// <summary>
        /// Get the role's name by employee.
        /// </summary>
        /// <param name="employeeId">Id of the employee</param>
        /// <returns>The role's name that match with the employeeId parameter.</returns>
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetRoleNameByEmployee(int employeeId)
        {
            string roleName = await _rolesRepository.GetNameByEmployerAsync(employeeId);
            return Ok(roleName);
        }

        /// <summary>
        /// Add a new roles
        /// </summary>
        /// <param name="rolesDTO">Roles's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoles([FromBody] RolesDTO rolesDTO)
        {
            Roles role = _mapper.Map<Roles>(rolesDTO);
            Roles createdRoles = await _rolesService.CreateAsync(role);
            rolesDTO = _mapper.Map<RolesDTO>(createdRoles);
            return Ok(rolesDTO);
        }

        /// <summary>
        /// Update an roles
        /// </summary>
        /// <param name="rolesDTO">Roles's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoles([FromBody] RolesDTO rolesDTO)
        {
            Roles role = _mapper.Map<Roles>(rolesDTO);
            Roles updatedRoles = _rolesService.Update(role);
            if (updatedRoles == null)
            {
                _validator.AddMessage("Role not found.");
                return NotFound();
            }

            rolesDTO = _mapper.Map<RolesDTO>(updatedRoles);
            return Ok(rolesDTO);
        }

        /// <summary>
        /// Delete an roles
        /// </summary>
        /// <param name="id">Roles's Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            Roles roles = _rolesService.Delete(id);
            if (roles == null)
            {
                _validator.AddMessage("Role not found. '-'");
                return NotFound();
            }

            RolesDTO rolesDTO = _mapper.Map<RolesDTO>(roles);
            return Ok(rolesDTO);
        }
    }
}