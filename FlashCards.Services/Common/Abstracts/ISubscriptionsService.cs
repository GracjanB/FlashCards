using FlashCards.Models.DTOs.Common;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlashCards.Services.Common.Abstracts
{
    public interface ISubscriptionsService
    {
        Task<SubscribedCourseDto> SubscribeCourse(int courseId, int accountId);

        bool UnsubscribeCourse(int subscribedCourseId);

        IEnumerable<SubscribedCourseDto> GetSubscribedCourses(int accountId,
            SubscribedCoursesParams subscribedCoursesParams, out PaginationHeader header);
    }
}
