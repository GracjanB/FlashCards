using FlashCards.Models.DTOs.ToClient.Learn;
using FlashCards.Services.Common.Abstracts;
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
    public class LearnController : ControllerBase
    {
        private readonly ILearnService _learnService;

        public LearnController(ILearnService learnService)
        {
            _learnService = learnService;
        }

        [HttpGet("course/{subCourseId}")]
        public IActionResult GetFlashcardsForLearn(int subCourseId)
        {
            var learnConfiguration = LoadDesignData();

            _learnService.DrawFlashcardsForLearn(1, 3);

            return Ok(learnConfiguration);
        }

        [HttpGet("course/{subCourseId}/lesson/{subLessonId}")]
        public IActionResult GetFlashcardsForLearnExactLesson(int subCourseId, int subLessonId)
        {
            var learnConfiguration = LoadDesignData();

            return Ok(learnConfiguration);
        }

        [HttpGet("repetition/course/{subCourseId}")]
        public IActionResult GetFlashcardsForRepetition(int subCourseId)
        {
            var learnConfiguration = LoadDesignData();

            return Ok(learnConfiguration);
        }

        [HttpGet("repetition/course/{subCourseId}/lesson/{subLessonId}")]
        public IActionResult GetFlashcardsForRepetitionExactLesson(int subCourseId, int subLessonId)
        {
            var learnConfiguration = LoadDesignData();

            return Ok(learnConfiguration);
        }

        [HttpPost("learnResult")]
        public IActionResult PostLearnResult(List<FlashcardForLearn> flashcards)
        {
            return Ok();
        }

        private LearnConfiguration LoadDesignData()
        {
            var learnConfiguration = new LearnConfiguration();

            var drawnFlashcards = new List<FlashcardForLearn>();
            drawnFlashcards.Add(new FlashcardForLearn
            {
                FlashcardId = 1,
                FlashcardSubscriptionId = 1,
                Phrase = "addictive",
                PhrasePronunciation = "adiktiw",
                TranslatedPhrase = "uzależniający",
                MarkedAsHard = false,
                TrainLevel = 4
            });
            drawnFlashcards.Add(new FlashcardForLearn
            {
                FlashcardId = 2,
                FlashcardSubscriptionId = 2,
                Phrase = "to look for",
                PhrasePronunciation = "tu luk for",
                TranslatedPhrase = "szukać",
                MarkedAsHard = false,
                TrainLevel = 2
            });
            drawnFlashcards.Add(new FlashcardForLearn
            {
                FlashcardId = 3,
                FlashcardSubscriptionId = 3,
                Phrase = "independent",
                PhrasePronunciation = "independent",
                TranslatedPhrase = "niezależny",
                MarkedAsHard = true,
                TrainLevel = 4
            });

            var flashcardsToLearn = new List<object>();
            flashcardsToLearn.Add(new FlashcardForLearnSelection
            {
                FlashcardId = 1,
                FlashcardSubscriptionId = 1,
                Phrase = "addictive",
                PhrasePronunciation = "adiktiw",
                TranslatedPhrase = "uzależniający",
                MarkedAsHard = false,
                TrainLevel = 4,
                FlashcardsForSelection = new List<string> { "zaradny", "niebezpieczny", "uzależniający", "inny" },
                CorrectPhrase = "uzależniający"
            });
            flashcardsToLearn.Add(new FlashcardForLearnSelection
            {
                FlashcardId = 2,
                FlashcardSubscriptionId = 2,
                Phrase = "to look for",
                PhrasePronunciation = "tu luk for",
                TranslatedPhrase = "szukać",
                MarkedAsHard = false,
                TrainLevel = 2,
                FlashcardsForSelection = new List<string> { "grzebać", "identyfikować", "użalać", "szukać" },
                CorrectPhrase = "szukać"
            });
            flashcardsToLearn.Add(new FlashcardForLearnInput
            {
                FlashcardId = 2,
                FlashcardSubscriptionId = 2,
                Phrase = "to look for",
                PhrasePronunciation = "tu luk for",
                TranslatedPhrase = "szukać",
                MarkedAsHard = false,
                TrainLevel = 2
            });
            flashcardsToLearn.Add(new FlashcardForLearnInput
            {
                FlashcardId = 1,
                FlashcardSubscriptionId = 1,
                Phrase = "addictive",
                PhrasePronunciation = "adiktiw",
                TranslatedPhrase = "uzależniający",
                MarkedAsHard = false,
                TrainLevel = 4
            });
            flashcardsToLearn.Add(new FlashcardForLearnSelection
            {
                FlashcardId = 3,
                FlashcardSubscriptionId = 3,
                Phrase = "independent",
                PhrasePronunciation = "independent",
                TranslatedPhrase = "niezależny",
                MarkedAsHard = true,
                TrainLevel = 4,
                FlashcardsForSelection = new List<string> { "robiony", "iunny", "makaron", "niezależny" },
                CorrectPhrase = "niezależny"
            });

            learnConfiguration.DrawnFlashcards = drawnFlashcards;
            learnConfiguration.FlashcardsForLearn = flashcardsToLearn;
            learnConfiguration.LearnType = 0;
            learnConfiguration.LessonName = "Angielski 6: Słownictwo biznesowe";
            return learnConfiguration;
        }
    }
}
