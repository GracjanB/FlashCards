using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMapper mapper, IAuthService authService, IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Get detail user information
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User detail information</returns>
        /// <response code="200">Detail information about user</response>
        /// <response code="400">When given user id doesn't exist in database</response>
        /// GET: /api/users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public IActionResult GetUser(int id)
        {
            var userFromRepo = _userRepository.GetDetail(id);

            if (userFromRepo == null)
                return BadRequest("User cannot be found.");

            var userToReturn = _mapper.Map<UserForDetail>(userFromRepo);

            return Ok(userToReturn);
        }

        /// <summary>
        /// List of users
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">List of users or empty list</response>
        /// GET: /api/users
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            List<UserForDetail> usersToReturn = new List<UserForDetail>();

            foreach (var user in users)
                usersToReturn.Add(_mapper.Map<UserForDetail>(user));

            return Ok(usersToReturn);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/users/{id}/changePassword
        ///         {
        ///             "oldPassword": "password",
        ///             "newPassword": "newbetterpassword",
        ///         }
        ///         
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="userForPasswordChange"></param>
        /// <returns>Status of operation</returns>
        /// <response code="200">When changing password was successful</response>
        /// <response code="401">When access token or old password was invalid</response>
        /// PUT /api/users/{id}/changePassword
        [HttpPut("{id}/changePassword")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> ChangePassword(int id, UserForPasswordChange userForPasswordChange)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Bad access token");

            var userFromRepo = _userRepository.Get(id);

            if (!_authService.VerifyPassword(userForPasswordChange.OldPassword, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return Unauthorized("Old password doesn't match");

            _authService.CreateNewPassword(userForPasswordChange.NewPassword, ref userFromRepo);

            try
            {
                await _userRepository.Update(userFromRepo);
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occured during change password", ex);
                return StatusCode(500);
            }

            return Ok("Password has been changed successfully");
        }
    }
}
