using System;

namespace FlashCards.Data.Models
{
    public class UserFlashcard
    {
        public int Id { get; set; }

        public DateTime LastTrainingDate { get; set; }

        public int LearningRate { get; set; }

        public bool IsLearned { get; set; }

        public bool MarkedAsHard { get; set; }


        public int UserLessonId { get; set; }

        public UserLesson UserLesson { get; set; }
    }
}
