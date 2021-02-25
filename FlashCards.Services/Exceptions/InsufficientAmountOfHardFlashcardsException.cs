using System;

namespace FlashCards.Services.Exceptions
{
    public class InsufficientAmountOfHardFlashcardsException : Exception
    {
        public InsufficientAmountOfHardFlashcardsException()
            : base() { }

        public InsufficientAmountOfHardFlashcardsException(string message)
            : base(message) { }

        public InsufficientAmountOfHardFlashcardsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
