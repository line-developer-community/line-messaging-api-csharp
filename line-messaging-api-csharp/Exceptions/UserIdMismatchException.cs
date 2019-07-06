using System;

namespace LineDC.Messaging.Exceptions
{
    /// <summary>
    /// Capture Invalid Signature Exception.
    /// </summary>
    public class UserIdMismatchException : Exception
    {
        public UserIdMismatchException()
        {
        }

        public UserIdMismatchException(string message) : base(message)
        {
        }

        public UserIdMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
