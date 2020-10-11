using System;

namespace FlashCards.Models.Exceptions
{
    [Serializable]
    public class FlashcardNotFoundException : Exception
    {
        public FlashcardNotFoundException() { }

        public FlashcardNotFoundException(string message)
            : base(message) { }

        public FlashcardNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}