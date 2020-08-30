using System;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IMapper mapper, ILogger<AuthController> logger)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates new account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/auth/login
        ///         {
        ///             "email": "sample@sample.com",
        ///             "password": "samplepassword",
        ///             "displayName": "sampleName",
        ///             "firstName": "someFirstName",
        ///             "lastName": "someLastName",
        ///             "city": "someCity",
        ///             "country": "someCountry"
        ///         }
        ///         
        /// </remarks>
        /// <param name="userForRegister"></param>
        /// <returns>Status</returns>
        /// <response code="200">If registration was successful</response>
        /// <response code="400">When incoming data was invalid. Returns model state information</response>
        /// <response code="500">When occured error during registration</response>
        /// POST: api/auth/register
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult Register(UserForRegister userForRegister)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userForRegister);
            user.UserInfo = _mapper.Map<UserInfo>(userForRegister);

            if (_authService.Register(user, userForRegister.Password))
            {
                _logger.LogInformation("New user has been registered");
                return Ok();
            }
                
            _logger.LogError("Error occured during register new user");
            throw new Exception("Error occured during register new user");
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/auth/login
        ///     {
        ///         "email": "sample@sample.com",
        ///         "password": "samplepassword"
        ///     }
        ///     
        /// </remarks>
        /// <param name="userForLogin"></param>
        /// <returns>Access token and detailed information about user</returns>
        /// <response code="200">Returns access token and detailed information about user</response>
        /// <response code="400">When incoming data was invalid. Returns model state information</response>
        /// <response code="401">When email or password was invalid</response>
        /// POST: api/auth/login
        [HttpPost("login", Name = "login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(userForLogin.Email, userForLogin.Password);

            if (result == null)
                return Unauthorized(new { error = "Password doesn't match." });

            _logger.LogInformation("New user has logged in");
            return Ok(result);
        }

    }
}
