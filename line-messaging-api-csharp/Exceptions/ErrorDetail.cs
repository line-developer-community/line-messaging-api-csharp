using System;
using System.Collections.Generic;
using System.Text;

namespace LineDC.Messaging.Exceptions
{
    /// <summary>
    /// Details of the error
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// Details of the error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Location of where the error occurred
        /// </summary>
        public string Property { get; set; }

        public ErrorDetail()
        {
        }

        public override string ToString()
        {
            return $"{{{Message}, {Property}}}";
        }
    }
}
