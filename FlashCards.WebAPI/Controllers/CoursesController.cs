using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Models.Exceptions;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
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
        private readonly ISubscriptionsService _subscriptionsService;
        private readonly IMapper _mapper;
        private readonly ILogger<CoursesController> _logger;
        private readonly IAdministrationService _administrationService;

        public CoursesController(ICourseRepository courseRepository, IUserRepository userRepository, 
            ISubscriptionsService subscriptionsService, IMapper mapper, ILogger<CoursesController> logger,
            IAdministrationService administrationService)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _subscriptionsService = subscriptionsService;
            _mapper = mapper;
            _logger = logger;
            _administrationService = administrationService;
        }

        /// <summary>
        /// Creates a new course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/courses
        ///         {
        ///             "name": "Course name",
        ///             "description": "Course sample description",
        ///             "courseType": 0, 
        ///             "accountId": 2
        ///         }
        /// 
        /// Additional information:
        ///     - For "courseType" -> (0 - public, 1 - private, 2 - draft)
        /// </remarks>
        /// <param name="courseForCreate">DTO</param>
        /// <returns>Status</returns>
        /// <response code="200">If creation was successful</response>
        /// <response code="400">If sent data was invalid</response>
        /// <response code="401">If token has expired or account id from token does not match with given in DTO</response>
        /// <response code="500">If occurred an error during creation of new course</response>
        /// POST: api/courses
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateCourse(CourseForCreate courseForCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var courseEntity = _mapper.Map<Course>(courseForCreate);

            if (await _courseRepository.Create(accountId, courseEntity))
            {
                var courseForReturn = _mapper.Map<CourseForDetail>(courseEntity);
                return Ok(courseForReturn);
            }

            return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during create new course. Try again later." });
        }

        /// <summary>
        /// Gets list of courses as paged list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/courses
        ///         {
        ///             "pageSize": 10,
        ///             "pageNumber": 1,
        ///             "courseType": 0
        ///         }
        /// 
        /// Additional information:
        ///     - For "courseType" -> (0 - public, 1 - private, 2 - draft)
        /// </remarks>
        /// <param name="courseParams">DTO</param>
        /// <returns>Status</returns>
        /// <response code="200">Returns paged list with additional information about pages in header "Pagination"</response>
        /// <response code="400">When incoming data was invalid. Returns model state information</response>
        /// <response code="401">If token has expired</response>
        /// GET: api/courses
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCourses([FromQuery] CourseParams courseParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courses = await _courseRepository.GetCourses(courseParams);
            var coursesForReturn = _mapper.Map<IEnumerable<CourseForList>>(courses);
            Response.AddPagination(courses.CurrentPage, courses.PageSize, courses.TotalCount, courses.TotalPages);

            return Ok(coursesForReturn);
        }

        /// <summary>
        /// Get detailed information about course
        /// </summary>
        /// <param name="id">Course ID</param>
        /// <returns>Status</returns>
        /// <response code="200">Returns detailed information about course</response>
        /// <response code="400">If course with given id was not found</response>
        /// <response code="401">If token has expired</response>
        /// GET: api/courses/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var courseFromRepo = await _courseRepository.GetDetail(id);

            if (courseFromRepo == null)
                return BadRequest(new ErrorResponse { ErrorMessage = "No course found with given id." });

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            if (_subscriptionsService.IsSubscribing(accountId, id, out int subscriptionId))
            {
                // var subscribedCourseDetail = _subscriptionsService.GetSubscribedCourseDetail(subscriptionId, id);

                // TODO
                var subscribedCourseDetail2 = _subscriptionsService.GetSubscribedCourseDetail2(subscriptionId, id);

                Console.WriteLine();
                return Ok(subscribedCourseDetail2);
            }
            else
            {
                var courseForDetailDto = _mapper.Map<CourseForDetail>(courseFromRepo);
                courseForDetailDto.IsSubscribing = false;
                return Ok(courseForDetailDto);
            }
        }

        [HttpGet("admin/forCheck")]
        [Produces("application/json")]
        public IActionResult GetCoursesForCheck()
        {
            if (_userRepository.IsAdministrator(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
            {
                var courses = _administrationService.GetCoursesForCheck();

                return Ok(courses);
            }
            else return Unauthorized();
        }

        [HttpGet("{id}/admin/forCheck")]
        [Produces("application/json")]
        public IActionResult GetCourseForCheck(int id)
        {
            if (_userRepository.IsAdministrator(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
            {
                try
                {
                    var courseToReturn = _administrationService.GetCourseForCheck(id);
                    return Ok(courseToReturn);
                }
                catch(CourseNotExistsException ex)
                {
                    _logger.LogError("Course not found.", ex);
                    return NotFound();
                }
            }
            else return Unauthorized();
        }

        [HttpPatch("{id}/admin/accept")]
        public IActionResult AcceptCourse(int id)
        {
            if (_userRepository.IsAdministrator(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
            {
                if (_administrationService.AcceptCourse(id))
                    return Ok();
                else 
                    return StatusCode(500);
            }
            else return Unauthorized();
        }

        [HttpPatch("{id}/admin/block")]
        public IActionResult BlockCourse(int id)
        {
            if (_userRepository.IsAdministrator(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
            {
                if (_administrationService.BlockCourse(id))
                    return Ok();
                else
                    return StatusCode(500);
            }
            else return Unauthorized();
        }

        /// <summary>
        /// Update information about course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/courses/{id}
        ///         {
        ///             "name": "New course name",
        ///             "description": "New course sample description",
        ///             "courseType": 0
        ///         }
        /// 
        /// Additional information:
        ///     - For "courseType" -> (0 - public, 1 - private, 2 - draft)
        /// </remarks>
        /// <param name="id">Course ID</param>
        /// <param name="courseForUpdate">DTO</param>
        /// <returns>Status</returns>
        /// <response code="200">If update was successful</response>
        /// <response code="400">If sent data was invalid (returns model state) or course with given id does not exists (returns error message)</response>
        /// <response code="401">If token has expired or user has no access to edit this course</response>
        /// <response code="500">If during update occurred an error</response>
        /// PUT: api/courses/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateCourse(int id, CourseForUpdate courseForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            if (!await _courseRepository.CanEdit(id, accountId))
                return Unauthorized();

            try
            {
                if (await _courseRepository.Update(id, courseForUpdate))
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
