using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = unitOfWork.GetUserRepository();
            _mapper = mapper;
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
    }
}
