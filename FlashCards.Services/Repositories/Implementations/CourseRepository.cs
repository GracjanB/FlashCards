using FlashCards.Data.DataModel;
using FlashCards.Data.Enums;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Models.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FlashcardsDataModel _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(FlashcardsDataModel context, ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Create(Course course)
        {
            try
            {
                course.CourseInfo = new CourseInfo() { Id = 0, AmountOfEnrolled = 0 };
                course.DateCreated = DateTime.Now;
                course.DateModified = DateTime.Now;
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during add new course");
                return false;
            }

            return true;
        }

        public async Task<bool> Update(CourseForUpdate course)
        {
            var courseFromRepo = _context.Courses.FirstOrDefault(x => x.Id == course.Id);

            if(courseFromRepo == null)
            {
                _logger.LogWarning($"Course with given id { course.Id } does not exists");
                throw new CourseNotFoundException();
            }

            try
            {
                courseFromRepo.Name = course.Name;
                courseFromRepo.Description = course.Description;
                courseFromRepo.CourseType = (CourseTypeEnum)course.CourseType;

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during update course");
                return false;
            }

            return true;
        }

        public async Task<PagedList<Course>> GetCourses(CourseParams courseParams)
        {
            var courses = _context.Courses
                .Include(x => x.AccountCreated)
                .Include(x => x.CourseInfo)
                .Include(x => x.Opinions)
                .Where(x => x.CourseType == (CourseTypeEnum)courseParams.CourseType)
                .AsQueryable();
                
            return await PagedList<Course>.CreateAsync(courses, courseParams.PageNumber, courseParams.PageSize);
        }

        public async Task<bool> CanEdit(int courseId, int accountId)
        {
            return await _context.Courses.AnyAsync(x => x.Id == courseId && x.AccountCreatedId == accountId);
        }
    }
}
