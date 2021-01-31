using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribedCourseManagerController : ControllerBase
    {
        private readonly ISubscribedCourseManagerService _courseManager;

        public SubscribedCourseManagerController(ISubscribedCourseManagerService courseManager)
        {
            _courseManager = courseManager ??
                throw new ArgumentNullException(nameof(courseManager));
        }

        [HttpPost("markFlashcardAsHard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult MarkFlashcardAsHard(SubscribedFlashcardMarkAsHard dto)
        {
            try
            {
                if(_courseManager.MarkFlashcardAsHard(dto.SubscribedFlashcardId, dto.AccountId, dto.MarkedAsHard))
                    return Ok();
                else 
                    return StatusCode(500, "An error occured during execute this method.");
            }
            catch(SubscribedFlashcardNotFoundException)
            {
                return NotFound("Flashcard with given id was not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured during execute this method.");
            }
        }

        [HttpPost("markFlashcardAsIgnored")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult MarkFlashcardAsIgnored(SubscribedFlashcardMarkAsIgnored dto)
        {
            try
            {
                if (_courseManager.MarkFlashcardAsIgnored(dto.SubscribedFlashcardId, dto.AccountId, dto.MarkAsIgnored))
                    return Ok();
                else
                    return StatusCode(500, "An error occured during execute this method.");
            }
            catch (SubscribedFlashcardNotFoundException)
            {
                return NotFound("Flashcard with given id was not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured during execute this method.");
            }
        }

        [HttpPost("clearProgressForFlashcard")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult ClearSubscribedFlashcardProgress(SubscribedFlashcardClearProgress dto)
        {
            try
            {
                if (_courseManager.ClearFlashcardProgress(dto.SubscribedFlashcardId, dto.AccountId))
                    return Ok();
                else
                    return StatusCode(500, "An error occured during execute this method.");
            }
            catch (SubscribedFlashcardNotFoundException)
            {
                return NotFound("Flashcard with given id was not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured during execute this method.");
            }
        }

        [HttpPost("clearProgressForLesson")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult ClearSubscribedLessonProgress(SubscribedLessonClearProgress dto)
        {
            try
            {
                if (_courseManager.ClearLessonProgress(dto.SubscribedLessonId, dto.AccountId))
                    return Ok();
                else
                    return StatusCode(500, "An error occured during execute this method.");
            }
            catch (SubscribedLessonNotFoundException)
            {
                return NotFound("Lesson with given id was not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured during execute this method.");
            }
        }

        [HttpPost("clearProgressForCourse")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult ClearSubscribedCourseProgress(SubscribedCourseClearProgress dto)
        {
            try
            {
                if (_courseManager.ClearCourseProgress(dto.SubscribedCoureId, dto.AccountId))
                    return Ok();
                else
                    return StatusCode(500, "An error occured during execute this method.");
            }
            catch (SubscribedCourseNotFoundException)
            {
                return NotFound("Course with given id was not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured during execute this method.");
            }
        }

    }
}
