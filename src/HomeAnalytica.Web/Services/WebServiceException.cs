namespace HomeAnalytica.Web.Services;

/// <summary>
/// Represents an exception that occurs during operations in the web service layer.
/// This exception is intended to handle errors related to web service calls,
/// such as HTTP request failures, deserialization issues, or other service-related problems.
/// </summary>
public class WebServiceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WebServiceException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public WebServiceException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebServiceException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not <c>null</c>,
    /// the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    public WebServiceException(string message, Exception innerException) : base(message, innerException) { }
}
