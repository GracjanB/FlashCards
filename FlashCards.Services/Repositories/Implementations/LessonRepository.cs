using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
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
    public class LessonRepository : ILessonRepository
    {
        private readonly FlashcardsDataModel _context;
        private readonly ILogger<LessonRepository> _logger;

        public LessonRepository(FlashcardsDataModel context, ILogger<LessonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Create(int courseId, Lesson lesson)
        {
            var course = _context.Courses.Include(x => x.Lessons).FirstOrDefault(x => x.Id == courseId);

            if(course == null)
            {
                _logger.LogWarning($"Course with given id { courseId } does not exists");
                throw new CourseNotFoundException();
            }

            try
            {
                lesson.DateCreated = DateTime.Now;
                lesson.DateModified = DateTime.Now;
                course.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during add new lesson to course (course id: { course.Id }");
                return false;
            }

            return true;
        }

        public async Task<bool> Create(int courseId, IEnumerable<Lesson> lessons)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var course = _context.Courses.FirstOrDefault(x => x.Id == courseId);

                if (course == null)
                {
                    _logger.LogWarning($"Course with given id { courseId } does not exists");
                    throw new CourseNotFoundException();
                }

                try
                {
                    foreach (var lesson in lessons)
                    {
                        lesson.DateCreated = DateTime.Now;
                        lesson.DateModified = DateTime.Now;
                        course.Lessons.Add(lesson);
                    }

                    _context.SaveChanges();
                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex, "An error occured during add range of lessons to course");
                    return false;
                }
            }

            return true;
        }

        public async Task<Lesson> Get(int id)
        {
            return await _context.Lessons.Include(x => x.Flashcards).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Lesson>> GetLessons(int courseId, LessonParams lessonParams)
        {
            var lessons = _context.Lessons.Where(x => x.CourseId == courseId).AsQueryable();

            return await PagedList<Lesson>.CreateAsync(lessons, lessonParams.PageNumber, lessonParams.PageSize);
        }

        public async Task<bool> Update(int lessonId, LessonForUpdate lessonForUpdate)
        {
            var lessonFromRepo = _context.Lessons.FirstOrDefault(x => x.Id == lessonId);

            if(lessonFromRepo == null)
            {
                _logger.LogWarning($"Lesson with given id { lessonId } does not exists");
                throw new LessonNotFoundException();
            }

            try
            {
                lessonFromRepo.DateModified = DateTime.Now;
                lessonFromRepo.Name = lessonForUpdate.Name;
                lessonFromRepo.Description = lessonForUpdate.Description;
                lessonFromRepo.Category = lessonForUpdate.Category;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"An error occured during update lesson with given id { lessonId }");
                return false;
            }

            return true;
        }
    }
}
