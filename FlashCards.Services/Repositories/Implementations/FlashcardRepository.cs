using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Models.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Implementations
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly FlashcardsDataModel _context;
        private readonly ILogger<FlashcardRepository> _logger;

        public FlashcardRepository(FlashcardsDataModel context, ILogger<FlashcardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Create(int lessonId, Flashcard flashcard)
        {
            var lesson = _context.Lessons.FirstOrDefault(x => x.Id == lessonId);

            if(lesson == null)
            {
                _logger.LogWarning($"Lesson with given id { lessonId } does not exists");
                throw new LessonNotFoundException();
            }

            try
            {
                flashcard.DateCreated = DateTime.Now;
                flashcard.DateModified = DateTime.Now;
                lesson.Flashcards.Add(flashcard);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during add new flashcard to lesson");
                return false;
            }

            return true;
        }

        public async Task<bool> Create(int lessonId, IEnumerable<Flashcard> flashcards)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var lesson = _context.Lessons.FirstOrDefault(x => x.Id == lessonId);

                if(lesson == null)
                {
                    _logger.LogWarning($"Lesson with given id { lessonId } does not exists");
                    throw new LessonNotFoundException();
                }

                try
                {
                    foreach(var flashcard in flashcards)
                    {
                        flashcard.DateCreated = DateTime.Now;
                        flashcard.DateModified = DateTime.Now;
                        lesson.Flashcards.Add(flashcard);
                    }

                    _context.SaveChanges();
                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "An error occurred during add range of flashcards to lesson");
                    return false;
                }
            }

            return true;
        }

        public async Task<List<Flashcard>> Get(int lessonId)
        {
            return await _context.Flashcards.Where(x => x.LessonId == lessonId).ToListAsync();
        }

        public async Task<bool> Update(Flashcard flashcard)
        {
            var flashcardFromRepo = _context.Flashcards.FirstOrDefault(x => x.Id == flashcard.Id);

            if(flashcardFromRepo == null)
            {
                _logger.LogWarning($"Flashcard with given id { flashcard.Id } does not exists");
                throw new FlashcardNotFoundException();
            }

            try
            {
                flashcardFromRepo.Phrase = flashcard.Phrase;
                flashcardFromRepo.PhrasePronunciation = flashcard.PhrasePronunciation;
                flashcardFromRepo.PhraseSampleSentence = flashcard.PhraseSampleSentence;
                flashcardFromRepo.PhraseComment = flashcard.PhraseComment;
                flashcardFromRepo.TranslatedPhrase = flashcard.TranslatedPhrase;
                flashcardFromRepo.TranslatedPhraseSampleSentence = flashcard.TranslatedPhraseSampleSentence;
                flashcardFromRepo.TranslatedPhraseComment = flashcard.TranslatedPhraseComment;
                flashcardFromRepo.LanguageLevel = flashcard.LanguageLevel;
                flashcardFromRepo.Category = flashcard.Category;
                flashcardFromRepo.DateModified = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during update flashcard with given id { flashcard.Id }");
                return false;
            }

            return true;
        }
    }
}
