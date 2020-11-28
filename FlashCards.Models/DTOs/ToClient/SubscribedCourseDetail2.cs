using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class SubscribedCourseDetail2
    {
        public int SubscriptionId { get; set; }

        public int CourseId { get; set; }

        public string AccountCreatedDisplayName { get; set; }

        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public int CourseType { get; set; }

        public string AmountOfEnrolled { get; set; }

        public bool IsSubscribing { get; set; }

        public int AmountOfFlashcards { get; set; }

        public int AmountOfFlashcardsLearnt { get; set; }

        public decimal OverallProgress { get; set; }

        public DateTime LastActivity { get; set; }

        public ICollection<SubscribedLessonForList> Lessons { get; set; }
    }
}
