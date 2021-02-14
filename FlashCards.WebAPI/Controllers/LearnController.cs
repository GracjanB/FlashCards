using FlashCards.Models.DTOs.ToClient.Learn;
using FlashCards.Services.Common.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        private readonly ILearnService _learnService;

        public LearnController(ILearnService learnService)
        {
            _learnService = learnService ??
                throw new ArgumentNullException(nameof(learnService));
        }

        /// <summary>
        /// Get flashcards for learn for selected course
        /// </summary>
        /// <param name="subCourseId">Selected course Id (subscription)</param>
        /// <returns>Learn configuration</returns>
        /// GET: api/learn/course/{subCourseId}
        [HttpGet("course/{subCourseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public IActionResult GetFlashcardsForLearn(int subCourseId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var learnConfiguration = _learnService.DrawFlashcardsForLearn(subCourseId, userId);

            if (learnConfiguration.DrawnFlashcards.Count() == 0)
                return NotFound("Not found any flashcards for learn");

            return Ok(learnConfiguration);
        }

        [HttpGet("course/{subCourseId}/lesson/{subLessonId}")]
        public IActionResult GetFlashcardsForLearnExactLesson(int subCourseId, int subLessonId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var learnConfiguration = _learnService.DrawFlashcardsForLearn(subCourseId, subLessonId, userId);

            if (learnConfiguration.DrawnFlashcards.Count() == 0)
                return NotFound("Not found any flashcards for learn");

            return Ok(learnConfiguration);
        }

        [HttpGet("repetition/course/{subCourseId}")]
        public IActionResult GetFlashcardsForRepetition(int subCourseId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repetitionConfiguration = _learnService.DrawFlashcardsForRepetition(subCourseId, userId);

            if (repetitionConfiguration.DrawnFlashcards.Count() == 0)
                return NotFound("Not found any flashcards for learn");

            return Ok(repetitionConfiguration);
        }

        [HttpGet("repetition/course/{subCourseId}/lesson/{subLessonId}")]
        public IActionResult GetFlashcardsForRepetitionExactLesson(int subCourseId, int subLessonId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repetitionConfiguration = _learnService.DrawFlashcardsForRepetition(subCourseId, userId, subLessonId);

            if (repetitionConfiguration.DrawnFlashcards.Count() == 0)
                return NotFound("Not found any flashcards for learn");

            return Ok(repetitionConfiguration);
        }

        [HttpPost("learnResult")]
        public IActionResult PostLearnResult(List<FlashcardForLearn> flashcards)
        {
            if (_learnService.SaveLearnResult(flashcards))
                return Ok();

            return StatusCode(500, new { ErrorMessage = "An error occured during save learn result. Try again later..."});
        }

        [HttpPost("repetitionResult")]
        public IActionResult PostRepetitionResult(List<FlashcardForLearn> flashcards)
        {
            if (_learnService.SaveRepetitionResult(flashcards))
                return Ok();

            return StatusCode(500, new { ErrorMessage = "An error occured during save repetition result. Try again later..." });
        }

    }
}
