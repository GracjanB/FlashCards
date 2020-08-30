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

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetUserRepository();
            _mapper = mapper;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAll();
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
        public IActionResult ChangePassword(int id, UserForPasswordChange userForPasswordChange)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Bad access token");

            var userFromRepo = _userRepository.Get(x => x.Id == id);

            if (!_authService.VerifyPassword(userForPasswordChange.OldPassword, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return Unauthorized("Old password doesn't match");

            _authService.CreateNewPassword(userForPasswordChange.NewPassword, ref userFromRepo);
            _userRepository.Update(userFromRepo);
            _unitOfWork.Save();

            return Ok("Password has been changed successfully");
        }
    }
}
