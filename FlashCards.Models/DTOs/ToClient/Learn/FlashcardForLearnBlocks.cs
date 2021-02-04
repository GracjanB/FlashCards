using System.Collections.Generic;

namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class FlashcardForLearnBlocks : FlashcardForLearn
    {
        public const string FlashcardType = "blocks";

        public List<string> DividedFlashcardPhraseShuffled { get; set; }

        public List<string> DividedFlashcardPhraseCorrect { get; set; }

        public FlashcardForLearnBlocks() { }

        public FlashcardForLearnBlocks(FlashcardForLearn flashcardForLearn) : base(flashcardForLearn) { }

        public FlashcardForLearnBlocks(FlashcardForLearn flashcardForLearn, 
            List<string> dividedFlashcardPhraseShuffled,
            List<string> dividedFlashcardPhraseCorrect) 
            : base(flashcardForLearn) 
        {
            DividedFlashcardPhraseShuffled = dividedFlashcardPhraseShuffled;
            DividedFlashcardPhraseCorrect = dividedFlashcardPhraseCorrect;
        }
    }
}
