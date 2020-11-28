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

        public SubscribedCourseDetailed GetSubscribedCourseDetail(int subscriptionId, int courseId)
        {
            var subscription = _context.SubscribedCourses.FirstOrDefault(x => x.Id == subscriptionId);
            var course = _context.Courses.FirstOrDefault(x => x.Id == courseId);

            if (subscription == null || course == null)
                return null;

            return CreateDetailedDto(subscription, course);
        }

        public SubscribedCourseDetail2 GetSubscribedCourseDetail2(int subscriptionId, int courseId)
        {
            SubscribedCourseDetail2 output = null;

            var subscribedCourseDetail = _context.SubscribedCourses.Include(x => x.Lessons)
                                                 .Join(_context.Courses.Include(x => x.AccountCreated).Include(x => x.Lessons),
                                                    subscription => subscription.CourseId,
                                                    course => course.Id,
                                                    (subscription, course) => new
                                                    {
                                                        SubscriptionId = subscription.Id,
                                                        CourseId = subscription.CourseId,
                                                        AccountCreatedDisplayName = course.AccountCreated.DisplayName,
                                                        CourseName = course.Name,
                                                        CourseDescription = course.Description,
                                                        CourseType = (int)course.CourseType,
                                                        IsSubscribing = subscription.IsSubscribed,
                                                        LastActivity = subscription.LastActivity,
                                                        SubscribedLesson = subscription.Lessons,
                                                        Lessons = course.Lessons
                                                    }).FirstOrDefault(x => x.SubscriptionId == subscriptionId && x.CourseId == courseId);

            if (subscribedCourseDetail != null)
            {
                output = new SubscribedCourseDetail2
                {
                    SubscriptionId = subscribedCourseDetail.SubscriptionId,
                    CourseId = subscribedCourseDetail.CourseId,
                    AccountCreatedDisplayName = subscribedCourseDetail.AccountCreatedDisplayName,
                    CourseName = subscribedCourseDetail.CourseName,
                    CourseDescription = subscribedCourseDetail.CourseDescription,
                    CourseType = subscribedCourseDetail.CourseType,
                    IsSubscribing = subscribedCourseDetail.IsSubscribing,
                    LastActivity = subscribedCourseDetail.LastActivity,
                    AmountOfEnrolled = default, // TODO
                    AmountOfFlashcards = default, // TODO
                    AmountOfFlashcardsLearnt = default, // TODO
                    OverallProgress = default, // TODO
                    Lessons = new List<SubscribedLessonForList>()
                };

                foreach (var lesson in subscribedCourseDetail.Lessons)
                {
                    var subscribedLessonInfo = subscribedCourseDetail.SubscribedLesson.FirstOrDefault(x => x.LessonId == lesson.Id);
                    var subscribedLessonForList = new SubscribedLessonForList
                    {
                        Id = lesson.Id,
                        Name = lesson.Name,
                        Progress = subscribedLessonInfo != null ? subscribedLessonInfo.OverallProgress : 0.00M
                    };
                    output.Lessons.Add(subscribedLessonForList);
                }
            }

            return output;
        }

        public LessonForDetail GetSubscribedLessonDetail(int subscriptionId, int lessonId)
        {
            LessonForDetail output = null;

            var subscription = _context.SubscribedCourses
                                       .Include(x => x.Lessons)
                                       .ThenInclude(x => x.SubscribedFlashcards)
                                       .FirstOrDefault(x => x.Id == subscriptionId);

            if (subscription != null)
            {
                var subscribedLesson = _context.SubscribedLessons
                                               .Include(x => x.SubscribedFlashcards)
                                               .FirstOrDefault(x => x.SubscribedCourseId == subscription.Id && x.LessonId == lessonId);

                var lesson = _context.Lessons
                                     .Include(x => x.Flashcards)
                                     .FirstOrDefault(x => x.Id == lessonId);

                output = new LessonForDetail
                {
                    Id = lesson.Id,
                    Name = lesson.Name,
                    Description = lesson.Description,
                    Category = lesson.Category,
                    DateCreated = lesson.DateCreated.ToShortDateString(),
                    DateModified = lesson.DateModified.ToShortDateString(),
                    IsSubscribed = subscription.IsSubscribed,
                    OverallProgress = subscribedLesson != null ? subscribedLesson.OverallProgress : 0.00M,
                    Flashcards = new List<FlashcardForList>()
                };

                foreach (var flashcard in lesson.Flashcards)
                {
                    SubscribedFlashcards subFlashcard = null;

                    if(subscribedLesson != null)
                        subFlashcard = subscribedLesson.SubscribedFlashcards.FirstOrDefault(x => x.FlashcardId == flashcard.Id);
                        
                    output.Flashcards.Add(new FlashcardForList
                    {
                        Id = flashcard.Id,
                        Phrase = flashcard.Phrase,
                        TranslatedPhrase = flashcard.TranslatedPhrase,
                        IsSubscribed = subscription.IsSubscribed,
                        Progress = subFlashcard != null ? subFlashcard.TrainLevel : 0.00M,
                        MarkedAsHard = subFlashcard != null ? subFlashcard.MarkedAsHard : false
                    });
                }
            }

            return output;
        }

        public IEnumerable<SubscribedCourseShort> GetSubscribedCourses(int accountId, SubscribedCoursesParams subscribedCoursesParams, out PaginationHeader header)
        {
            var subscribedCoursesFromRepo = _context.SubscribedCourses
                                                    .Where(x => x.AccountId == accountId && x.IsSubscribed == true)
                                                    .OrderByDescending(x => x.LastActivity)
                                                    .AsQueryable();

            var coursesPagedList = PagedList<SubscribedCourse>.CreateAsync(subscribedCoursesFromRepo,
                subscribedCoursesParams.PageNumber, subscribedCoursesParams.PageSize).Result;
            var coursesId = subscribedCoursesFromRepo.Select(x => x.CourseId).ToList();
            var coursesFromRepo = _context.Courses.Include(x => x.AccountCreated).Where(x => coursesId.Contains(x.Id)).ToList();
            header = new PaginationHeader(coursesPagedList.CurrentPage, coursesPagedList.PageSize, coursesPagedList.TotalCount, coursesPagedList.TotalPages);

            var coursesToReturn = _mapper.Map<IEnumerable<SubscribedCourseShort>>(coursesPagedList);
            foreach (var course in coursesToReturn)
                course.Course = _mapper.Map<CourseShort>(coursesFromRepo.First(x => x.Id == course.CourseId));

            return coursesToReturn;
        }

        public bool IsSubscribing(int accountId, int courseId, out int subscriptionId)
        {
            var subscription = _context.SubscribedCourses.FirstOrDefault(x => x.AccountId == accountId && x.CourseId == courseId && x.IsSubscribed == true);

            if (subscription != null)
            {
                subscriptionId = subscription.Id;
                return true;
            }
            else
            {
                subscriptionId = default;
                return false;
            }
        }

        public async Task<SubscribedCourseShort> SubscribeCourse(int courseId, int accountId)
        {
            if (!_courseRepository.Exists(courseId))
                throw new CourseNotExistsException();

            var subscribedCourse = _context.SubscribedCourses.FirstOrDefault(x => x.CourseId == courseId &&
                                                                                  x.AccountId == accountId &&
                                                                                  x.IsSubscribed == false);

            if (subscribedCourse != null)
                subscribedCourse.IsSubscribed = true;
            else if (subscribedCourse == null)
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
            catch (Exception ex)
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

        private SubscribedCourseShort CreateDto(SubscribedCourse subscribedCourse, Course course)
        {
            var subscribedCourseDto = _mapper.Map<SubscribedCourseShort>(subscribedCourse);
            subscribedCourseDto.Course = _mapper.Map<CourseShort>(course);

            return subscribedCourseDto;
        }

        private SubscribedCourseDetailed CreateDetailedDto(SubscribedCourse subscribedCourse, Course course)
        {
            return new SubscribedCourseDetailed
            {
                Id = course.Id,
                AccountCreatedId = course.AccountCreatedId,
                Name = course.Name,
                Description = course.Description,
                CourseType = (int)course.CourseType,
                DateCreated = course.DateCreated.ToShortDateString(),
                DateModified = course.DateModified.ToShortDateString(),
                Lessons = _mapper.Map<ICollection<LessonForList>>(course.Lessons),
                SubscriptionId = subscribedCourse.Id,
                LastActivity = subscribedCourse.LastActivity,
                OverallProgress = subscribedCourse.OverallProgress
            };
        }
    }
}
