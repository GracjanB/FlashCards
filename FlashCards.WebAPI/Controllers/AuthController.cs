using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegister userForRegister)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userForRegister);
            user.UserInfo = _mapper.Map<UserInfo>(userForRegister);

            if (!_authService.Register(user, userForRegister.Password))
                return BadRequest(new { error = "An error occured during registering. Please try again later" });

            return Ok();
        }

        [HttpPost("login", Name = "login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(userForLogin.Email, userForLogin.Password);

            if (result == null)
                return BadRequest(new { error = "Password doesn't match." });

            return Ok(result);
        }


    }
}
