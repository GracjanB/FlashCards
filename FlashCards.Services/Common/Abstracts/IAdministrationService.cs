using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Services.Common.Abstracts
{
    public interface IAdministrationService
    {
        List<CourseForCheck> GetCoursesForCheck();

        CourseForCheck GetCourseForCheck(int courseId);

        bool AcceptCourse(int courseId);

        bool BlockCourse(int courseId);
    }
}
