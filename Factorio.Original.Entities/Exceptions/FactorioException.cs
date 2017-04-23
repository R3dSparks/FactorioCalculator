using System;

namespace Factorio.Entities
{
    /// <summary>
    /// The Exception class for this application
    /// </summary>
    public class FactorioException : Exception
    {
        private const string defaultMessage = "Factorio Exception was thrown";


        /// <summary>
        /// Code which spesifice where the error occurred
        /// </summary>
        public int ErrorCode { get; set; }


        /// <summary>
        /// create an empty exception
        /// </summary>
        public FactorioException() : this(DiagnosticEvents.Base, defaultMessage, null) { }

        /// <summary>
        /// create an empty exception and an error code to define where the exception comes from.
        /// </summary>
        /// <param name="errorCode">defines where the exception comes from</param>
        public FactorioException(int errorCode) : this(errorCode, defaultMessage, null) { }

        /// <summary>
        /// create an exception with a message and an error code to define where the exception comes from.
        /// </summary>
        /// <param name="errorCode">defines where the exception comes from</param>
        /// <param name="message">error message</param>
        public FactorioException(int errorCode, string message) : this(errorCode, message, null) { }

        /// <summary>
        /// create an exception with a message, an inner exception and an error code to define where the exception comes from.
        /// </summary>
        /// <param name="errorCode">defines where the exception comes from</param>
        /// <param name="message">error message</param>
        /// <param name="innerException">the source exception</param>
        public FactorioException(int errorCode, string message, Exception innerException) : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }

    }
}
