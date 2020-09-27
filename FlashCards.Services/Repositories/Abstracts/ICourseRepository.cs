using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.UnitOfWork.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Abstracts
{
    public interface ICourseRepository 
    {
        Task Create(Course course);

        Task Update(Course course);

        Task<PagedList<Course>> GetCourses(CourseParams courseParams);

        Task<Course> GetCourseForUpdate(int courseId, int accountId);
    }
}
