using AutoMapper;
using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Common.Abstracts;
using FlashCards.Services.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Services.Common.Implementations
{
    public class SubscriptionService : ISubscriptionsService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly FlashcardsDataModel _context;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(ICourseRepository courseRepository,
            FlashcardsDataModel context, IMapper mapper, 
            ILogger<SubscriptionService> logger)
        {
            _courseRepository = courseRepository ??
                throw new ArgumentNullException(nameof(courseRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _context = context;
        }

        public IEnumerable<SubscribedCourseDto> GetSubscribedCourses(int accountId, SubscribedCoursesParams subscribedCoursesParams, out PaginationHeader header)
        {
            var subscribedCoursesFromRepo = _context.SubscribedCourses
                                                    .Where(x => x.AccountId == accountId && x.IsSubscribed == true)
                                                    .OrderByDescending(x => x.LastActivity)
                                                    .AsQueryable();

            var coursesPagedList = PagedList<SubscribedCourse>.CreateAsync(subscribedCoursesFromRepo, 
                subscribedCoursesParams.PageNumber, subscribedCoursesParams.PageSize).Result;
            var coursesId = subscribedCoursesFromRepo.Select(x => x.CourseId).ToList();
            var coursesFromRepo = _context.Courses.Include(x => x.AccountCreated.DisplayName).Where(x => coursesId.Contains(x.Id)).ToList();
            header = new PaginationHeader(coursesPagedList.CurrentPage, coursesPagedList.PageSize, coursesPagedList.TotalCount, coursesPagedList.TotalPages);
            
            var coursesToReturn = _mapper.Map<IEnumerable<SubscribedCourseDto>>(coursesPagedList);
            foreach(var course in coursesToReturn)
                course.Course = _mapper.Map<CourseShort>(coursesFromRepo.First(x => x.Id == course.CourseId));

            return coursesToReturn;
        }

        public async Task<SubscribedCourseDto> SubscribeCourse(int courseId, int accountId)
        {
            if (!_courseRepository.Exists(courseId))
                throw new CourseNotExistsException();

            var subscribedCourse = _context.SubscribedCourses.FirstOrDefault(x => x.CourseId == courseId && 
                                                                                  x.AccountId == accountId && 
                                                                                  x.IsSubscribed == false);

            if(subscribedCourse != null)
                subscribedCourse.IsSubscribed = true;
            else if(subscribedCourse == null)
            {
                subscribedCourse = new SubscribedCourse
                {
                    Id = 0,
                    LastActivity = DateTime.Now,
                    OverallProgress = 0.00M,
                    IsSubscribed = true,
                    AccountId = accountId,
                    CourseId = courseId
                };

                _context.SubscribedCourses.Add(subscribedCourse);
            }

            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error during add new subscription");
                return null;
            }

            var courseFromRepo = await _courseRepository.Get(courseId);
            var subscribedCourseDto = CreateDto(subscribedCourse, courseFromRepo);

            return subscribedCourseDto;
        }

        public bool UnsubscribeCourse(int subscribedCourseId)
        {
            var output = false;
            var subscribedCourse = _context.SubscribedCourses.FirstOrDefault(x => x.Id == subscribedCourseId);

            if (subscribedCourse != null)
            {
                subscribedCourse.IsSubscribed = false;

                try
                {
                    _context.SaveChanges();
                    output = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error during unsubscribe course.\n Subscription id: { subscribedCourseId }");
                }
            }
            else throw new SubscriptionNotFoundException();

            return output;
        }

        private SubscribedCourseDto CreateDto(SubscribedCourse subscribedCourse, Course course)
        {
            var subscribedCourseDto = _mapper.Map<SubscribedCourseDto>(subscribedCourse);
            subscribedCourseDto.Course = _mapper.Map<CourseShort>(course);

            return subscribedCourseDto;
        }
    }
}
