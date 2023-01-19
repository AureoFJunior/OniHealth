using System.Collections.Generic;
using System.Linq;
using OniHealth.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OniHealth.Domain.DTOs;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using OniHealth.Domain;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using AutoMapper;
using OniHealth.Web.Config;

namespace OniHealth.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService<User> _userService;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public UserController(IUserService<User> userService,
            IRepository<User> userRepository,
            IMapper mapper,
            IValidator validator)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
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
            List<User> users = _userRepository.GetAll().ToList();
            if (users == null)
            {
                _validator.AddMessage("User not found.");
                return NotFound();
            }

            foreach (User userUpdate in users)
            {
                userUpdate.IsLogged = 0;
                _userService.Update(userUpdate);
            }

            User user = users.Where(x => x != null && x.UserName == userName && x.Password == password).FirstOrDefault();
            if (users == null)
            {
                _validator.AddMessage("User or password incorrect.");
                return NotFound();
            }

            string token, refreshToken;
            GenerateToken(user, out token, out refreshToken);

            if (String.IsNullOrEmpty(token) || String.IsNullOrEmpty(refreshToken))
                throw new Exception();

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
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            if (users == null)
            {
                _validator.AddMessage("Users not found.");
                return NotFound();
            }

            IEnumerable<UserDTO> userDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDTO);
        }

        /// <summary>
        /// Get a user.
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>The user that match with the id parameter.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _validator.AddMessage("User not found.");
                return NotFound();
            }

            IEnumerable<UserDTO> userDTO = _mapper.Map<IEnumerable<UserDTO>>(user);
            return Ok(userDTO);
        }

        /// <summary>
        ///  Get a logged user.
        /// </summary>
        /// <returns>The logged user.</returns>
        [HttpGet]
        public async Task<IActionResult> IsLogged()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            if (users == null)
            {
                _validator.AddMessage("User not found.");
                return NotFound();
            }

            UserDTO user = users.Where(x => x.IsLogged == 1).Select(x => new UserDTO { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, BirthDate = x.BirthDate, Token = "" }).FirstOrDefault();
            return Ok(user);
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
            User createdUser = await _userService.CreateAsync(user);
            return Ok(createdUser);
        }

        /// <summary>
        /// Update an user
        /// </summary>
        /// <param name="userDTO">User to be updated</param>
        /// <returns>The updated user</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            User updatedUser = _userService.Update(user);

            if (updatedUser == null)
            {
                _validator.AddMessage("User not found.");
                return NotFound();
            }

            userDTO = _mapper.Map<UserDTO>(updatedUser);
            return Ok(userDTO);
        }

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>The deleted user</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = _userService.Delete(id);
            if (user == null)
            {
                _validator.AddMessage("User not found.");
                return NotFound();
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }
    }
}