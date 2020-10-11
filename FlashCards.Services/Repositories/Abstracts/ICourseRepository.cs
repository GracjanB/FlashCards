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
        Task<bool> Create(Course course);

        Task<bool> Update(CourseForUpdate courseForUpdate);

        Task<PagedList<Course>> GetCourses(CourseParams courseParams);

        Task<bool> CanEdit(int courseId, int accountId);
    }
}
