using System;

namespace FlashCards.Services.Exceptions
{
    public class CourseNotExistsException : Exception
    {
        public CourseNotExistsException() 
            : base() { }

        public CourseNotExistsException(string message) 
            : base(message) { }

        public CourseNotExistsException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
