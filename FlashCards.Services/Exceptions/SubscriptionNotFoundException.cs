using System;

namespace FlashCards.Services.Exceptions
{
    public class SubscriptionNotFoundException : Exception
    {
        public SubscriptionNotFoundException()
            : base() { }

        public SubscriptionNotFoundException(string message)
            : base(message) { }

        public SubscriptionNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
