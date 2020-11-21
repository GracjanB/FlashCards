using System;

namespace FlashCards.Models.DTOs.ToClient
{
    public class SubscribedCourseDetailed : CourseForDetail
    {
        public int SubscriptionId { get; set; }

        public DateTime LastActivity { get; set; }

        public decimal OverallProgress { get; set; }
    }
}
