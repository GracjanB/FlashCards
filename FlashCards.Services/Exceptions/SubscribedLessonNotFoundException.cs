using System;

namespace FlashCards.Services.Exceptions
{
    public class SubscribedLessonNotFoundException : Exception
    {
        public SubscribedLessonNotFoundException()
            : base() { }

        public SubscribedLessonNotFoundException(string message)
            : base(message) { }

        public SubscribedLessonNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
