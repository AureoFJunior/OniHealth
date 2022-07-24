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
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly IRepository<User> _userRepository;

        public UserController(UserService userService,
            IRepository<User> userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Return all the users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                IEnumerable<User> users = _userRepository.GetAll();

                IEnumerable<UserDTO> user = users.Where(x => x != null).Select(x => new UserDTO { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, BirthDate = x.BirthDate, Token = "" });

                if (!user.Any())
                    return NotFound(new { message = $"Usuários não encontrados." });

                return Ok(user);

            }catch(Exception ex) { return Problem($"Erro ao buscar registros de Usuários: {ex.Message}"); }
        }

        /// <summary>
        /// Get a user.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>The user that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                if (user == null)
                {
                    return NotFound(new { message = $"Usuário de id={id} não encontrado" });
                }
                return Ok(user);
            }
            catch (Exception ex) { return Problem($"Erro ao buscar registro de Usuário: {ex.Message}"); }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>The added user</returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                User createdUser = await _userService.CreateAsync(user);

                return Ok(user);

            } catch (Exception ex){ return Problem($"Erro ao criar registro de Usuário: {ex.Message}");}
        }

        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <returns>The updated user</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                User updatedUser = _userService.Update(user);
                return Ok(user);

            }
            catch (Exception ex) { return Problem($"Erro ao atualizar registro de Usuário: {ex.Message}"); }
        }

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>The deleted user</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                User user = _userService.Delete(id);
                return Ok(user);

            }
            catch (Exception ex) { return Problem($"Erro ao remover registro de Usuário: {ex.Message}"); }
        }
    }
}