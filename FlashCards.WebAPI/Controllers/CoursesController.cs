using AutoMapper;
using FlashCards.Data.Enums;
using FlashCards.Data.Models;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, IUserRepository userRepository, 
            IMapper mapper, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseForCreate courseForCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = _userRepository.GetDetail(courseForCreate.UserId);

            if (courseForCreate.UserId != userId || user == null)
                return Unauthorized();

            var courseEntity = _mapper.Map<Course>(courseForCreate);
            courseEntity.AccountCreatedId = user.UserInfoId;

            try
            {
                await _courseRepository.Create(courseEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured during save new course", ex);
                return StatusCode(500);
            }
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(CourseParams courseParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (courseParams.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var courses = await _courseRepository.GetCourses(courseParams);
            var coursesForReturn = _mapper.Map<IEnumerable<CourseForList>>(courses);
            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);

            return Ok(coursesForReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseForUpdate courseForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (courseForUpdate.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var accountId = _userRepository.GetUserAccountId(courseForUpdate.UserId);
            var courseFromRepo = await _courseRepository.GetCourseForUpdate(id, accountId);

            if (courseFromRepo == null)
                return Unauthorized();

            courseFromRepo.Name = courseForUpdate.Name;
            courseFromRepo.Description = courseForUpdate.Description;
            courseFromRepo.CourseType = (CourseTypeEnum)courseForUpdate.CourseType;

            try
            {
                await _courseRepository.Update(courseFromRepo);
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occured during updating course", ex);
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
