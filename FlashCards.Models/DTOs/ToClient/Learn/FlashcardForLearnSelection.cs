using System;
using System.Collections.Generic;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    [Serializable]
    public class FlashcardForLearnSelection : FlashcardForLearn
    {
        public string FlashcardType { get; set; } = "selection";

        public List<string> FlashcardsForSelection { get; set; }

        public string CorrectPhrase { get; set; }

        public FlashcardForLearnSelection() { }

        public FlashcardForLearnSelection(FlashcardForLearn flashcardForLearn) 
            : base(flashcardForLearn) { }

        public FlashcardForLearnSelection(FlashcardForLearn flashcardForLearn, 
            List<string> flashcardsForSelection, string correctPhrase) : base(flashcardForLearn)
        {
            FlashcardsForSelection = flashcardsForSelection;
            CorrectPhrase = correctPhrase;
        }
    }
}
