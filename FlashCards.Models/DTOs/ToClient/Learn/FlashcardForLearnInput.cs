namespace FlashCards.Models.DTOs.ToClient.Learn
{
    public class FlashcardForLearnInput : FlashcardForLearn
    {
        public const string FlashcardType = "input";

        public FlashcardForLearnInput() { }
        
        public FlashcardForLearnInput(FlashcardForLearn flashcardForLearn) : base(flashcardForLearn) { }
    }
}
