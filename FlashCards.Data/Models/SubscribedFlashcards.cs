using System;

namespace FlashCards.Data.Models
{
    public class SubscribedFlashcards
    {
        public int Id { get; set; }

        public DateTime LastTrainingDate { get; set; }

        public int TrainLevel { get; set; }

        public int DifficultyLevel { get; set; }

        public bool MarkedAsHard { get; set; }


        public int FlashcardId { get; set; }

        public int SubscribedLessonId { get; set; }

        public SubscribedLesson SubscribedLesson { get; set; }
    }
}
