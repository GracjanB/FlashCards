namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class FlashcardForLearnPresentation : FlashcardForLearn
    {
        public const string FlashcardType = "presentation";

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
