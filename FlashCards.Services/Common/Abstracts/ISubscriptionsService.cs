using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlashCards.Services.Common.Abstracts
{
    public interface ISubscriptionsService
    {
        Task<SubscribedCourseShort> SubscribeCourse(int courseId, int accountId);

        bool UnsubscribeCourse(int subscribedCourseId);

        IEnumerable<SubscribedCourseShort> GetSubscribedCourses(int accountId,
            SubscribedCoursesParams subscribedCoursesParams, out PaginationHeader header);

        bool IsSubscribing(int accountId, int courseId, out int subscriptionId);

        SubscribedCourseDetailed GetSubscribedCourseDetail(int subscriptionId, int courseId);

        SubscribedCourseDetail2 GetSubscribedCourseDetail2(int subscriptionId, int courseId);

        LessonForDetail GetSubscribedLessonDetail(int subscriptionId, int lessonId);
    }
}
