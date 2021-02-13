using AutoMapper;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Services.Common.Implementations
{
    public class AdministrationService : IAdministrationService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AdministrationService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public bool AcceptCourse(int courseId)
        {
            return _courseRepository.ChangeCourseStatus(courseId, Data.Enums.CourseStatusEnum.Accepted);
        }

        public bool BlockCourse(int courseId)
        {
            return _courseRepository.ChangeCourseStatus(courseId, Data.Enums.CourseStatusEnum.Blocked);
        }

        public List<CourseForCheck> GetCoursesForCheck()
        {
            var coursesForCheck = new List<CourseForCheck>();
            var coursesEntity = _courseRepository.GetDetailedCoursesForCheck().Result;

            foreach (var courseEntity in coursesEntity)
                coursesForCheck.Add(_mapper.Map<CourseForCheck>(courseEntity));

            return coursesForCheck;
        }

        public CourseForCheck GetCourseForCheck(int courseId)
        {
            var courseForCheck = _courseRepository.GetDetailedCourseForCheck(courseId).Result;
            if (courseForCheck != null)
            {
                return _mapper.Map<CourseForCheck>(courseForCheck);
            }
            else throw new CourseNotExistsException();
        }


    }
}
