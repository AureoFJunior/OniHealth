using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Web.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using OniHealth.Infra.Repositories;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly RolesService _rolesService;
        private readonly IRepositoryRoles _rolesRepository;

        public RolesController(RolesService rolesService,
            IRepositoryRoles rolesRepository)
        {
            _rolesService = rolesService;
            _rolesRepository = rolesRepository;
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
                IEnumerable<Roles> roless = await _rolesRepository.GetAllAsync();

                IEnumerable<RolesDTO> roles = roless.Where(x => x != null).Select(x => new RolesDTO { Id = x.Id, Name = x.Name });

                if (!roles.Any())
                    return NotFound(new { message = $"Cargos não encontrados." });

                return Ok(roles);

            }catch(Exception ex) { return Problem($"Erro ao buscar registros de Cargos: {ex.Message}"); }
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
                    return NotFound(new { message = $"Cargo de id={id} não encontrado" });
                }
                return Ok(roles);
            }
            catch (Exception ex) { return Problem($"Erro ao buscar registro de Cargo: {ex.Message}"); }
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
                    return NotFound(new { message = $"Cargo de id={id} não encontrado" });
                }
                return Ok(roleName);
            }
            catch (Exception ex) { return Problem($"Erro ao buscar registro de Cargo: {ex.Message}"); }
        }

        /// <summary>
        /// Add a new roles
        /// </summary>
        /// <param name="roles">Roles's to be added</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoles([FromBody] RolesDTO rolesDTO)
        {
            try
            {
                Roles role = new Roles() { Id = rolesDTO.Id, Name = rolesDTO.Name };
                Roles createdRoles = await _rolesService.CreateAsync(role);

                return Ok(createdRoles);

            } catch (Exception ex){ return Problem($"Erro ao criar registro de Cargo: {ex.Message}");}
        }

        /// <summary>
        /// Update an roles
        /// </summary>
        /// <param name="roles">Roles's to be updated.</param>
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
            catch (Exception ex) { return Problem($"Erro ao atualizar registro de Cargo: {ex.Message}"); }
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
            catch (Exception ex) { return Problem($"Erro ao remover registro de Cargo: {ex.Message}"); }
        }
    }
}