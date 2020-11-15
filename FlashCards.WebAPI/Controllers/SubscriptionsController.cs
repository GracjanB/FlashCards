using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Helpers.Extensions;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionsService _subscriptionsService;
        private readonly ILogger<SubscriptionsController> _logger;
        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionsService subscriptionsService,
            IUserRepository userRepository, ILogger<SubscriptionsController> logger,
            IMapper mapper)
        {
            _subscriptionsService = subscriptionsService ??
                throw new ArgumentNullException(nameof(subscriptionsService));

            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get subscribed courses as paged list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/subscriptions
        ///         {
        ///             "pageSize": 10,
        ///             "pageNumber": 1
        ///         }
        ///
        /// </remarks>
        /// <param name="subscribedCoursesParams"></param>
        /// <response code="200">Returns subscribed courses</response>
        /// <returns>Status</returns>
        /// GET api/subscriptions
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public IActionResult GetCoursesPaged([FromQuery] SubscribedCoursesParams subscribedCoursesParams)
        {
            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var coursesToReturn = _subscriptionsService.GetSubscribedCourses(accountId, subscribedCoursesParams, out PaginationHeader header);
            Response.AddPagination(header.CurrentPage, header.ItemsPerPage, header.TotalItems, header.TotalPages);

            return Ok(coursesToReturn);
        }

        /// <summary>
        /// Subscribe course
        /// </summary>
        /// <param name="courseId">Course ID</param>
        /// <returns>Status</returns>
        /// <response code="200">Returns subscribed course details</response>
        /// <response code="400">When incoming data was invalid. Returns model state information</response>
        /// POST api/subscriptions/subscribe/{courseId}
        [HttpPost("subscribe/{courseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public IActionResult SubscribeCourse(int courseId)
        {
            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            try
            {
                var subscribedCourseDto = _subscriptionsService.SubscribeCourse(courseId, accountId).Result;

                if (subscribedCourseDto != null)
                    return Ok(subscribedCourseDto);
                else
                    return StatusCode(500, $"Error occured during subscribe course.\n Account Id: {accountId}\nCourse Id: {courseId}");
            }
            catch (CourseNotExistsException)
            {
                return BadRequest("Course with given id does not exists.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error during subscribe course");
                return StatusCode(500, $"Error occured during subscribe course.\n Account Id: {accountId}\nCourse Id: {courseId}");
            }
        }

        /// <summary>
        /// Unsubscribe course
        /// </summary>
        /// <param name="id">Subscription ID</param>
        /// <response code="200">Returned if unsubscribe course completed successfully</response>
        /// <response code="400">When incoming data was invalid. Returns model state information</response>
        /// <returns>Status</returns>
        /// PUT api/subscriptions/unsubscribe/{id}
        [HttpPut("unsubscribe/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public IActionResult UnsubscribeCourse(int id)
        {
            try
            {
                if (_subscriptionsService.UnsubscribeCourse(id))
                    return Ok();
                else
                    return StatusCode(500, $"Error during unsubscribe course.\nCourse Id: {id}");
            }
            catch(SubscriptionNotFoundException)
            {
                return BadRequest("Subscription with given id does not exists.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error during unsubscribe course.\nCourse Id: {id}");
                return StatusCode(500, $"Error during unsubscribe course.\nCourse Id: {id}");
            }
        }

        [HttpPut("clearProgress/{id}")]
        public async Task<IActionResult> ClearLearningProgress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
