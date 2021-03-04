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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<bool> Create(int accountId, Course course)
        {
            try
            {
                course.CourseInfo = new CourseInfo() { Id = 0, AmountOfEnrolled = 0 };
                course.DateCreated = DateTime.Now;
                course.DateModified = DateTime.Now;
                course.AccountCreatedId = accountId;

                if (course.CourseType == CourseTypeEnum.Draft || course.CourseType == CourseTypeEnum.Private)
                    course.Status = CourseStatusEnum.NotAccepted;
                else if (course.CourseType == CourseTypeEnum.Public)
                    course.Status = CourseStatusEnum.Pending;

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

        public async Task<bool> Update(int courseId, CourseForUpdate course)
        {
            var courseFromRepo = _context.Courses.FirstOrDefault(x => x.Id == courseId);

            if(courseFromRepo == null)
            {
                _logger.LogWarning($"Course with given id { courseId } does not exists");
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
                .Where(x => x.CourseType == (CourseTypeEnum)courseParams.CourseType && x.Status == CourseStatusEnum.Accepted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(courseParams.SearchedTitle) && !string.IsNullOrWhiteSpace(courseParams.SearchedTitle))
                courses = courses.Where(x => x.Name.Contains(courseParams.SearchedTitle));
                
            return await PagedList<Course>.CreateAsync(courses, courseParams.PageNumber, courseParams.PageSize);
        }

        public async Task<Course> Get(int id)
        {
            return await _context.Courses.Include(x => x.CourseInfo).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> GetDetail(int id)
        {
            return await _context.Courses
                                 .Include(x => x.CourseInfo)
                                 .Include(x => x.Lessons)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> GetDetailedForCheck(int id)
        {
            return await _context.Courses
                                .Include(x => x.Lessons)
                                .ThenInclude(x => x.Flashcards)
                                .Include(x => x.AccountCreated)
                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Course>> GetDetailedCoursesForCheck()
        {
            return await _context.Courses
                                .Include(x => x.Lessons)
                                .ThenInclude(x => x.Flashcards)
                                .Include(x => x.AccountCreated)
                                .Where(x => x.Status == CourseStatusEnum.Pending && x.CourseType == CourseTypeEnum.Public)
                                .ToListAsync();
        }

        public async Task<Course> GetDetailedCourseForCheck(int courseId)
        {
            return await _context.Courses
                .Include(x => x.Lessons)
                .ThenInclude(x => x.Flashcards)
                .Include(x => x.AccountCreated)
                .FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<bool> CanEdit(int courseId, int accountId)
        {
            return await _context.Courses.AnyAsync(x => x.Id == courseId && x.AccountCreatedId == accountId);
        }

        public bool Exists(int id)
        {
            return _context.Courses.Any(x => x.Id == id);
        }

        public bool ChangeCourseStatus(int id, CourseStatusEnum status)
        {
            var course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course != null)
            {
                course.Status = status;

                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    _logger.LogError("Problem with save course", ex);
                    return false;
                }

            }
            else return false;
        }
    }
}
