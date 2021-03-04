using System.Collections.Generic;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class RepetitionConfiguration
    {
        public ICollection<FlashcardForLearn> DrawnFlashcards { get; set; }

        public ICollection<FlashcardForLearnInput> FlashcardsForLearn { get; set; }

        public int LearnType { get; set; }

        public string LessonName { get; set; }
    }
}
