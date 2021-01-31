using FlashCards.Data.DataModel;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FlashCards.Services.Common.Implementations
{
    public class SubscribedCourseManagerService : ISubscribedCourseManagerService
    {
        private readonly FlashcardsDataModel _context;
        private readonly ILogger<SubscribedCourseManagerService> _logger;

        public SubscribedCourseManagerService(FlashcardsDataModel context, ILogger<SubscribedCourseManagerService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool ClearCourseProgress(int subscribedCourseId, int accountId)
        {
            bool output = false;

            var subscribedCourse = _context.SubscribedCourses.FirstOrDefault(x => x.Id == subscribedCourseId && x.AccountId == accountId);

            if (subscribedCourse != null)
            {
                foreach (var subscribedLesson in subscribedCourse.Lessons)
                {
                    subscribedLesson.OverallProgress = 0;
                    subscribedLesson.LastTrainingDate = new DateTime();

                    foreach (var subscribedFlashcard in subscribedLesson.SubscribedFlashcards)
                    {
                        subscribedFlashcard.LastRevisionDate = new DateTime();
                        subscribedFlashcard.LastTrainingDate = new DateTime();
                        subscribedFlashcard.TrainLevel = 0;
                        subscribedFlashcard.MarkedAsHard = false;
                        subscribedFlashcard.MarkedAsIgnored = false;
                    }
                }

                subscribedCourse.LastActivity = DateTime.Now;
                subscribedCourse.OverallProgress = 0;

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured during save. Method name: ClearCourseProgress", ex);
                }
            }
            else throw new SubscribedCourseNotFoundException();

            return output;
        }

        public bool ClearFlashcardProgress(int subscribedFlashcardId, int accountId)
        {
            bool output = false;

            var subscribedFlashcard = _context.SubscribedFlashcards
                .FirstOrDefault(x => x.Id == subscribedFlashcardId && x.SubscribedLesson.SubscribedCourse.AccountId == accountId);

            if(subscribedFlashcard != null)
            {
                subscribedFlashcard.LastRevisionDate = new DateTime();
                subscribedFlashcard.LastTrainingDate = new DateTime();
                subscribedFlashcard.TrainLevel = 0;
                subscribedFlashcard.MarkedAsHard = false;
                subscribedFlashcard.MarkedAsIgnored = false;

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured during save. Method name: ClearFlashcardProgress", ex);
                }
            }
            else throw new SubscribedFlashcardNotFoundException();

            return output;
        }

        public bool ClearLessonProgress(int subscribedLessonId, int accountId)
        {
            bool output = false;

            var subscribedLesson = _context.SubscribedLessons
                .FirstOrDefault(x => x.Id == subscribedLessonId && x.SubscribedCourse.AccountId == accountId);

            if (subscribedLesson != null)
            {
                foreach (var flashcard in subscribedLesson.SubscribedFlashcards)
                {
                    flashcard.LastRevisionDate = new DateTime();
                    flashcard.LastTrainingDate = new DateTime();
                    flashcard.TrainLevel = 0;
                    flashcard.MarkedAsHard = false;
                    flashcard.MarkedAsIgnored = false;
                }

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured during save. Method name: ClearLessonProgress", ex);
                }
            }
            else throw new SubscribedLessonNotFoundException();

            return output;
        }

        public bool MarkFlashcardAsHard(int subscribedFlashcardId, int accountId, bool isHard)
        {
            bool output = false;

            var subscribedFlashcard = _context.SubscribedFlashcards
                .FirstOrDefault(x => x.Id == subscribedFlashcardId && x.SubscribedLesson.SubscribedCourse.AccountId == accountId);

            if (subscribedFlashcard != null)
            {
                subscribedFlashcard.MarkedAsHard = isHard;

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch(Exception ex)
                {
                    _logger.LogError("An error occured during save. Method name: MarkFlashcardAsHard", ex);
                }
            }
            else throw new SubscribedFlashcardNotFoundException();

            return output;
        }

        public bool MarkFlashcardAsIgnored(int subscribedFlashcardId, int accountId, bool isIgnored)
        {
            bool output = false;

            var subscribedFlashcard = _context.SubscribedFlashcards
                .FirstOrDefault(x => x.Id == subscribedFlashcardId && x.SubscribedLesson.SubscribedCourse.AccountId == accountId);

            if (subscribedFlashcard != null)
            {
                subscribedFlashcard.MarkedAsIgnored = isIgnored;

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occured during save. Method name: MarkFlashcardAsIgnored", ex);
                }
            }
            else throw new SubscribedFlashcardNotFoundException();

            return output;
        }
    }
}
