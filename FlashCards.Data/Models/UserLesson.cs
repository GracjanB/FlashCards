using System;
using System.Collections.Generic;

namespace FlashCards.Data.Models
{
    public class UserLesson
    {
        public int Id { get; set; }

        public int ProgressPercentage { get; set; }

        public DateTime LastTrainingDate { get; set; }


        public int UserCourseId { get; set; }

        public UserCourse UserCourse { get; set; }

        public ICollection<UserFlashcard> Flashcards { get; set; }
    }
}
