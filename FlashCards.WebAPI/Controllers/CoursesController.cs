using AutoMapper;
using FlashCards.Data.Enums;
using FlashCards.Data.Models;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Models.Exceptions;
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
            var accountId = _userRepository.GetUserAccountId(userId);

            if (courseForCreate.AccountId != accountId)
                return Unauthorized();

            var courseEntity = _mapper.Map<Course>(courseForCreate);

            if (await _courseRepository.Create(courseEntity))
                return Ok();

            return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during create new course. Try again later." });
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(CourseParams courseParams)
        {
            throw new NotImplementedException();

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //if (courseParams.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //    return Unauthorized();

            //var courses = await _courseRepository.GetCourses(courseParams);
            //var coursesForReturn = _mapper.Map<IEnumerable<CourseForList>>(courses);
            //Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);

            //return Ok(coursesForReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseForUpdate courseForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accountId = _userRepository.GetUserAccountId(userId);

            if (courseForUpdate.AccountId != accountId || !await _courseRepository.CanEdit(courseForUpdate.Id, courseForUpdate.AccountId))
                return Unauthorized();

            try
            {
                if (await _courseRepository.Update(courseForUpdate))
                    return Ok();

                return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during update course. Try again later." });
            }
            catch (CourseNotFoundException)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = "Course has not been found." });
            }
        }
    }
}
