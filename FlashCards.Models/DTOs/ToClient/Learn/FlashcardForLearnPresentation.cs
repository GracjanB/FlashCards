using System;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    [Serializable]
    public class FlashcardForLearnPresentation : FlashcardForLearn
    {
        public string FlashcardType { get; set; } = "presentation";

        public bool WithInput { get; set; }

        public FlashcardForLearnPresentation() { }

        public FlashcardForLearnPresentation(FlashcardForLearn flashcardForLearn) : base(flashcardForLearn) { }

        public FlashcardForLearnPresentation(FlashcardForLearn flashcardForLearn, bool withInput) 
            : base(flashcardForLearn) 
        {
            WithInput = withInput;
        }
    }
}
