namespace HomeAnalytica.Web.Services;

/// <summary>
/// Enum representing the different types of toast notifications.
/// Each type corresponds to a different visual style and message meaning.
/// </summary>
public enum ToastNotificationType
{
    /// <summary>
    /// Represents a success toast notification, typically used for indicating a successful action.
    /// </summary>
    Success,

    /// <summary>
    /// Represents an informational toast notification, typically used for providing non-critical information.
    /// </summary>
    Info,

    /// <summary>
    /// Represents a warning toast notification, typically used for highlighting potential issues or cautions.
    /// </summary>
    Warning,

    /// <summary>
    /// Represents an error toast notification, typically used for showing an error or failure message.
    /// </summary>
    Error
}

