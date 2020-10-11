using System;

namespace FlashCards.Models.Exceptions
{
    [Serializable]
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException() { }

        public CourseNotFoundException(string message) 
            : base(message) { }

        public CourseNotFoundException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}