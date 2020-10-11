using System;

namespace FlashCards.Models.Exceptions
{
    [Serializable]
    public class LessonNotFoundException : Exception
    {
        public LessonNotFoundException() { }

        public LessonNotFoundException(string message)
            : base(message) { }

        public LessonNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}