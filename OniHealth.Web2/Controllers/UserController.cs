using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Web.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using OniHealth.Domain;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
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
        /// Log into the system and get the API token.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("{userName}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> LogInto(string userName, string password)
        {
            try
            {
                List<User> users = _userRepository.GetAll().ToList();

                foreach(User userUpdate in users)
                {
                    userUpdate.IsLogged = 0;
                    _userService.Update(userUpdate);
                }

                User user = users.Where(x => x != null && x.UserName == userName && x.Password == password).FirstOrDefault();

                if (user == null)
                    return NotFound(new { message = $"Usuários não encontrados." });

                string token, refreshToken;
                GenerateToken(user, out token, out refreshToken);

                if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(refreshToken))
                    return Problem($"Não foi possível autenticar o usuário {user.UserName}.");

                user.IsLogged = 1;
                _userService.Update(user);

                UserDTO loggedUser = new UserDTO()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Token = token,
                    RefreshToken = refreshToken
                };

                return Ok(loggedUser);

            }
            catch (Exception ex) { return Problem($"Erro ao autenticar o usuário: {ex.Message}"); }
        }

        private static void GenerateToken(User user, out string token, out string refreshToken)
        {
            token = TokenService.GenerateToken(user);
            refreshToken = TokenService.GenerateRefreshToken();
            TokenService.SaveRefreshToken(user.UserName, refreshToken);
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
                IEnumerable<User> users = await _userRepository.GetAllAsync();
                IEnumerable<UserDTO> user = users.Where(x => x != null).Select(x => new UserDTO { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, BirthDate = x.BirthDate, Token = "" });

                return Ok(user);
            }
            catch (NotFoundDatabaseException ex) { return NotFound(new { message = $"Users not found." }); }
            catch (Exception ex) { return Problem($"Error at users search: {ex.Message}"); }
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
                User user = await _userRepository.GetByIdAsync(id);
                return Ok(user);
            }
            catch (NotFoundDatabaseException ex) { return NotFound(new { message = $"The user with te ID={id} was not found." }); }
            catch (Exception ex) { return Problem($"Error at user search: {ex.Message}"); }
        }

        /// <summary>
        ///  Get a logged user.
        /// </summary>
        /// <returns>The logged user.</returns>
        [HttpGet]
        public async Task<IActionResult> IsLogged()
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetAllAsync();
                UserDTO user = users.Where(x => x.IsLogged == 1).Select(x => new UserDTO { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, BirthDate = x.BirthDate, Token = "" }).FirstOrDefault();

                return Ok(user);
            }
            catch (NotFoundDatabaseException ex) { return NotFound(new { message = $"Logged user was not found." }); }
            catch (Exception ex) { return Problem($"Error at logged user search: {ex.Message}"); }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>The added user</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                User createdUser = await _userService.CreateAsync(user);
                return Ok(createdUser);
            }
            catch (ConflictDatabaseException ex) { return Conflict(new { message = $"User {user.UserName} already exists in the database" }); }
            catch (Exception ex) { return Problem($"Error at user creation: {ex.Message}"); }
        }

        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <returns>The updated user</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                User updatedUser = _userService.Update(user);
                return Ok(updatedUser);
            }
            catch (NotFoundDatabaseException ex) { return NotFound(new { message = $"The user with te ID={user.Id} was not found." }); }
            catch (Exception ex) { return Problem($"Error at user update: {ex.Message}"); }
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
            catch (NotFoundDatabaseException ex) { return NotFound(new { message = $"The user with te ID={id} was not found." }); }
            catch (Exception ex) { return Problem($"Error while deleting user: {ex.Message}"); }
        }
    }
}