using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Models.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashCards.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/courses/{courseId}/lessons/{lessonId}/flashcards")]
    [ApiController]
    public class FlashcardsController : ControllerBase
    {
        private readonly IFlashcardRepository _flashcardsRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashcardsController> _logger;

        public FlashcardsController(IFlashcardRepository flashcardRepository, ICourseRepository courseRepository,
            IMapper mapper, ILogger<FlashcardsController> logger, IUserRepository userRepository)
        {
            _flashcardsRepository = flashcardRepository ?? throw new ArgumentNullException(nameof(flashcardRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all flashcards from lesson
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        /// GET: api/courses/{courseId}/lessons/{lessonId}/flashcards
        [HttpGet]
        public async Task<IActionResult> GetFlashcards(int lessonId)
        {
            var flashcardsFromRepo = await _flashcardsRepository.Get(lessonId);
            var flashcardsToReturn = _mapper.Map<IEnumerable<FlashcardForDetail>>(flashcardsFromRepo);

            return Ok(flashcardsToReturn);
        }

        /// <summary>
        /// Get flashcard
        /// </summary>
        /// <param name="id">Flashcard ID</param>
        /// <returns></returns>
        /// GET: api/courses/{courseId}/lessons/{lessonId}/flashcards/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlashcard(int id)
        {
            var flashcardFromRepo = await _flashcardsRepository.GetFlashcard(id);
            var flashcardToReturn = _mapper.Map<FlashcardForDetail>(flashcardFromRepo);

            return Ok(flashcardToReturn);
        }

        /// <summary>
        /// Creates new flashcard to lesson
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/courses/34/lessons/232/flashcards
        ///         {
        ///             "phrase": "Additional",
        ///             "phrasePronunciation": "əˈdiSH(ə)n(ə)l",
        ///             "phraseSampleSentence": "I have an additional monitor for work",
        ///             "phraseComment": "",
        ///             "translatedPhrase": "Dodatkowy",
        ///             "translatedPhraseSampleSentence": "Mam dodatkowy monitor do pracy",
        ///             "translatedPhraseComment": "",
        ///             "languageLevel": 2,
        ///             "category": "miscellaneous"
        ///         }
        ///         
        /// </remarks>
        /// <param name="courseId">Course ID</param>
        /// <param name="lessonId">Lesson ID</param>
        /// <param name="flashcardForCreate">Flashcard DTO</param>
        /// <response code="200">If create was successful</response>
        /// <response code="400">If sent data was invalid (returns model state)</response>
        /// <response code="401">If token has expired or user has no access to edit this course</response>
        /// <response code="404">If lesson with given id has not been found</response>
        /// <response code="500">If during create occurred an error</response>
        /// <returns>Status</returns>
        /// POST api/courses/{courseId}/lessons/{lessonId}/flashcards
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateFlashcard(int courseId, int lessonId, FlashcardForCreate flashcardForCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (!await _courseRepository.CanEdit(courseId, accountId))
                return Unauthorized();

            var flashcardEntity = _mapper.Map<Flashcard>(flashcardForCreate);

            try
            {
                if (await _flashcardsRepository.Create(lessonId, flashcardEntity))
                    return Ok();

                return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during create flashcard. Try again later." });
            }
            catch(LessonNotFoundException)
            {
                return NotFound(new ErrorResponse { ErrorMessage = "Lesson has not been found." });
            }
        }

        /// <summary>
        /// Updates flashcard data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/courses/34/lessons/232/flashcards
        ///         {
        ///             "phrase": "Additional",
        ///             "phrasePronunciation": "əˈdiSH(ə)n(ə)l",
        ///             "phraseSampleSentence": "I have an additional monitor for work",
        ///             "phraseComment": "",
        ///             "translatedPhrase": "Dodatkowy",
        ///             "translatedPhraseSampleSentence": "Mam dodatkowy monitor do pracy",
        ///             "translatedPhraseComment": "",
        ///             "languageLevel": 2,
        ///             "category": "miscellaneous"
        ///         }
        ///         
        /// </remarks>
        /// <param name="courseId">Course ID</param>
        /// <param name="id">Flashcard ID</param>
        /// <param name="flashcardForUpdate">Flashcard DTO</param>
        /// <response code="200">If create was successful</response>
        /// <response code="400">If sent data was invalid (returns model state)</response>
        /// <response code="401">If token has expired or user has no access to edit this course</response>
        /// <response code="404">If lesson with given id has not been found</response>
        /// <response code="500">If during create occurred an error</response>
        /// <returns>Status</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateFlashcard(int courseId, int id, FlashcardForUpdate flashcardForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountId = _userRepository.GetUserAccountId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (!await _courseRepository.CanEdit(courseId, accountId))
                return Unauthorized();

            try
            {
                if (await _flashcardsRepository.Update(id, flashcardForUpdate))
                    return Ok();

                return StatusCode(500, new ErrorResponse { ErrorMessage = "An error occurred during update flashcard. Try again later." });
            }
            catch(FlashcardNotFoundException)
            {
                return NotFound(new ErrorResponse { ErrorMessage = "Flashcard has not been found." });
            }
        }
    }
}
