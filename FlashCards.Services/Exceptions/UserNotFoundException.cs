using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Services.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base() { }

        public UserNotFoundException(string message)
            : base(message) { }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
