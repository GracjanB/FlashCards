using System;

namespace FlashCards.Services.Exceptions
{
    public class SubscribedCourseNotFoundException : Exception
    {
        public SubscribedCourseNotFoundException()
            : base() { }

        public SubscribedCourseNotFoundException(string message)
            : base(message) { }

        public SubscribedCourseNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
