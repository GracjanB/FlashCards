using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class LearnConfiguration
    {
        public ICollection<FlashcardForLearn> DrawnFlashcards { get; set; }

        public ICollection<object> FlashcardsForLearn { get; set; }

        public int LearnType { get; set; }

        public string LessonName { get; set; }
    }
}
