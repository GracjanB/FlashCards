using System;

namespace FlashCards.Services.Exceptions
{
    public class SubscribedFlashcardNotFoundException : Exception
    {
        public SubscribedFlashcardNotFoundException()
            : base() { }

        public SubscribedFlashcardNotFoundException(string message)
            : base(message) { }

        public SubscribedFlashcardNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
