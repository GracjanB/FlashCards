using System;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class SubscribedLesson
    {
        public int Id { get; set; }

        public decimal OverallProgress { get; set; }

        public DateTime LastTrainingDate { get; set; }


        public int LessonId { get; set; }

        public int SubscribedCourseId { get; set; }

        public SubscribedCourse SubscribedCourse { get; set; }

        public ICollection<SubscribedFlashcards> SubscribedFlashcards { get; set; }
    }
}
