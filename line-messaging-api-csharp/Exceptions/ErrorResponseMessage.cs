using System.Collections.Generic;

namespace LineDC.Messaging.Exceptions
{
    /// <summary>
    /// Error returned from LINE Platform
    /// https://developers.line.me/en/docs/messaging-api/reference/#error-responses
    /// </summary>
    public class ErrorResponseMessage
    {
        /// <summary>
        /// Summary of the error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Details of the error
        /// </summary>
        public IList<ErrorDetail> Details { get; set; }

        public ErrorResponseMessage()
        {}

        public override string ToString()
        {
            return (Details == null) ? Message : $"{Message},[{string.Join(",", Details)}]";
        }

    }
}
