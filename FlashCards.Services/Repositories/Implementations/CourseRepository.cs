using FlashCards.Data.DataModel;
using FlashCards.Data.Enums;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FlashcardsDataModel _context;

        public CourseRepository(FlashcardsDataModel context)
        {
            _context = context;
        }

        public async Task Create(Course course)
        {
            course.CourseInfo = new CourseInfo() { Id = 0, AmountOfEnrolled = 0 };
            course.DateCreated = DateTime.Now;
            course.DateModified = DateTime.Now;
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
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

        public async Task<Course> GetCourseForUpdate(int courseId, int accountId)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId && x.AccountCreatedId == accountId);
        }
    }
}
