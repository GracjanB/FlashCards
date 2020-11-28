using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Models.Exceptions;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/courses/{courseId}/lessons")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISubscriptionsService _subscriptionsService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(ILessonRepository lessonRepository, ICourseRepository courseRepository,
            IMapper mapper, ILogger<LessonsController> logger, IUserRepository userRepository,
            ISubscriptionsService subscriptionsService)
        {
            _lessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subscriptionsService = subscriptionsService ?? throw new ArgumentNullException(nameof(subscriptionsService));
        }

        /// <summary>
        /// Get details about lesson
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="id">Lesson ID</param>
        /// <returns>Status</returns>
        /// <response code="200">Detailed info about lesson</response>
        /// <response code="401">If token has expired</response>
        /// <response code="404">If lesson has been not found</response>
        /// GET: api/courses/{courseId}/lessons/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetLessonDetail(int courseId, int id)
        {
            LessonForDetail lessonToReturn = null;

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            if (_subscriptionsService.IsSubscribing(accountId, courseId, out int subscriptionId))
                lessonToReturn = _subscriptionsService.GetSubscribedLessonDetail(subscriptionId, id);
            else
            {
                var lessonFromRepo = await _lessonRepository.Get(id);
                lessonToReturn = _mapper.Map<LessonForDetail>(lessonFromRepo);

                // TODO: Think about how to change the way to inform client about subscription
                lessonToReturn.IsSubscribed = false;
                foreach (var flashcard in lessonToReturn.Flashcards)
                    flashcard.IsSubscribed = false;
            }

            return Ok(lessonToReturn);
        }

        /// <summary>
        /// Get paged list of courses
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/courses/34/lessons
        ///         {
        ///             "pageSize": 10,
        ///             "pageNumber": 1,
        ///             "category": "home"
        ///         }
        /// 
        /// </remarks>
        /// <param name="courseId">Course ID</param>
        /// <param name="lessonParams">Paged list parameters</param>
        /// <response code="200">Returns paged list of lessons</response>
        /// <response code="400">If incoming data was invalid</response>
        /// <response code="401">If token has expired</response>
        /// <returns>Status</returns>
        /// GET: /api/courses/{courseId}/lessons
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> GetLessons(int courseId, LessonParams lessonParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lessons = await _lessonRepository.GetLessons(courseId, lessonParams);
            var lessonsForReturn = _mapper.Map<IEnumerable<LessonForList>>(lessons);
            Response.AddPagination(lessons.CurrentPage, lessons.PageSize, lessons.TotalCount, lessons.TotalPages);

            return Ok(lessonsForReturn);
        }

        /// <summary>
        /// Creates new lesson in course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/courses/23/lessons
        ///         {
        ///             "name": "Things in house",
        ///             "description": "The lesson contains vocabulary for use in house",
        ///             "category": "house"
        ///         }
        ///         
        /// </remarks>
        /// <param name="courseId">Course id where new lesson will be created</param>
        /// <param name="lessonForCreate">DTO</param>
        /// <response code="200">If creation was successfull</response>
        /// <response code="400">If incoming data was invalid</response>
        /// <response code="401">If token has expired or user has no ability to add new lesson to course</response>
        /// <response code="404">If course with given id was not found</response>
        /// <response code="500">If occurred error during creation a new lesson</response>
        /// <returns>Status</returns>
        /// POST: api/courses/{courseId}/lessons
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateLesson(int courseId, LessonForCreate lessonForCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var accountId = _userRepository.GetUserAccountId(userId);

            if (!await _courseRepository.CanEdit(courseId, accountId))
                return Unauthorized();

            var lessonEntity = _mapper.Map<Lesson>(lessonForCreate);

            try
            {
                if (await _lessonRepository.Create(courseId, lessonEntity))
                {
                    var lessonToReturn = _mapper.Map<LessonForDetail>(lessonEntity);
                    return Ok(lessonToReturn);
                }

                return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during create new lesson. Try again later." });
            }
            catch (CourseNotFoundException)
            {
                return NotFound(new ErrorResponse { ErrorMessage = "Course has not been found." });
            }
        }

        /// <summary>
        /// Updates lesson data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/courses/23/lessons/546
        ///         {
        ///             "name": "New lesson name",
        ///             "description": "New description for lesson",
        ///             "category": "law"
        ///         }
        ///         
        /// </remarks>
        /// <param name="courseId">Course ID</param>
        /// <param name="id">Lesson ID</param>
        /// <param name="lessonForUpdate">New data for lesson</param>
        /// <response code="200">If update was successfull</response>
        /// <response code="400">If incoming data was invalid</response>
        /// <response code="401">If token has expired or user has no ability to add new lesson to course</response>
        /// <response code="404">If lesson with given id was not found</response>
        /// <response code="500">If occurred error during update a new lesson</response>
        /// <returns>Status</returns>
        /// PUT: api/courses/{courseId}/lessons/{lessonId}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateLesson(int courseId, int id, LessonForUpdate lessonForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            if (!await _courseRepository.CanEdit(courseId, accountId))
                return Unauthorized();

            try
            {
                if (await _lessonRepository.Update(id, lessonForUpdate))
                    return Ok();

                return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during update new lesson. Try again later." });
            }
            catch (LessonNotFoundException)
            {
                return NotFound(new ErrorResponse { ErrorMessage = "Lesson has not been found." });
            }
        }
    }
}
