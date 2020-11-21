using System;

namespace FlashCards.Models.DTOs.ToClient
{
    public class SubscribedCourseShort
    {
        public int Id { get; set; }

        public DateTime LastActivity { get; set; }

        public decimal OverallProgress { get; set; }

        public int CourseId { get; set; }

        public CourseShort Course { get; set; }
    }
}
