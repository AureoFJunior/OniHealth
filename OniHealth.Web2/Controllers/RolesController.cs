using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using OniHealth.Infra.Repositories;
using AutoMapper;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly RolesService _rolesService;
        private readonly IRepositoryRoles _rolesRepository;
        private readonly IMapper _mapper;


        public RolesController(RolesService rolesService,
            IRepositoryRoles rolesRepository,
            IMapper mapper)
        {
            _rolesService = rolesService;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all the roless.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                IEnumerable<Roles> roles = await _rolesRepository.GetAllAsync();

                IEnumerable<RolesDTO> rolesDTO = _mapper.Map<IEnumerable<RolesDTO>>(roles);

                if (!rolesDTO.Any())
                    return NotFound(new { message = $"Roles not found." });

                return Ok(rolesDTO);

            }catch(Exception ex) { return Problem($"Error at roles search"); }
        }

        /// <summary>
        /// Get a roles.
        /// </summary>
        /// <param name="id">Id of the roles</param>
        /// <returns>The roles that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                Roles roles = await _rolesRepository.GetByIdAsync(id);
                if (roles == null)
                {
                    return NotFound(new { message = $"The role with ID={id} was not found." });
                }
                return Ok(roles);
            }
            catch (Exception ex) { return Problem($"Error at role search"); }
        }

        /// <summary>
        /// Get the role's name.
        /// </summary>
        /// <param name="id">Id of the roles</param>
        /// <returns>The role's name that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleName(int id)
        {
            try
            {
                string roleName= await _rolesRepository.GetNameByIdAsync(id);
                if (String.IsNullOrEmpty(roleName))
                {
                    return NotFound(new { message = $"The role with ID={id} was not found." });
                }
                return Ok(roleName);
            }
            catch (Exception ex) { return Problem($"Error at role search"); }
        }

        /// <summary>
        /// Get the role's name by employee.
        /// </summary>
        /// <param name="employeeId">Id of the employee</param>
        /// <returns>The role's name that match with the employeeId parameter.</returns>
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetRoleNameByEmployee(int employeeId)
        {
            try
            {
                string roleName = await _rolesRepository.GetNameByEmployerAsync(employeeId);
                if (String.IsNullOrEmpty(roleName))
                {
                    return NotFound(new { message = $"The role with ID={employeeId} was not found." });
                }
                return Ok(roleName);
            }
            catch (Exception ex) { return Problem($"Error at role search"); }
        }

        /// <summary>
        /// Add a new roles
        /// </summary>
        /// <param name="rolesDTO">Roles's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoles([FromBody] RolesDTO rolesDTO)
        {
            try
            {
                Roles role = new Roles() { Id = rolesDTO.Id, Name = rolesDTO.Name };
                Roles createdRoles = await _rolesService.CreateAsync(role);

                return Ok(createdRoles);

            } catch (Exception ex){ return Problem($"Error at role creation");}
        }

        /// <summary>
        /// Update an roles
        /// </summary>
        /// <param name="rolesDTO">Roles's to be updated.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoles([FromBody] RolesDTO rolesDTO)
        {
            try
            {
                Roles role = new Roles() { Id = rolesDTO.Id, Name = rolesDTO.Name };
                Roles updatedRoles = _rolesService.Update(role);
                return Ok(updatedRoles);

            }
            catch (Exception ex) { return Problem($"Error at role update"); }
        }

        /// <summary>
        /// Delete an roles
        /// </summary>
        /// <param name="id">Roles's Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            try
            {
                Roles roles = _rolesService.Delete(id);
                return Ok(roles);

            }
            catch (Exception ex) { return Problem($"Error while deleting role"); }
        }
    }
}