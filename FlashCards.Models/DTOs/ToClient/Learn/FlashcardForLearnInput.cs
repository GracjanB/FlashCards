using System;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    [Serializable]
    public class FlashcardForLearnInput : FlashcardForLearn
    {
        public string FlashcardType { get; set; } = "input";

        public FlashcardForLearnInput() { }
        
        public FlashcardForLearnInput(FlashcardForLearn flashcardForLearn) : base(flashcardForLearn) { }
    }
}
