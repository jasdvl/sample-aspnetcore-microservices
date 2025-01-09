namespace HomeAnalytica.Web.Services;

/// <summary>
/// A service that manages toast notifications in the application.
/// It triggers the display and automatic hiding of toasts after a specified duration.
/// </summary>
public class ToastNotificationService : IDisposable
{
    // Event triggered when a toast is shown with the provided message, title, and type.
    public event Action<string, string, ToastNotificationType>? OnShow;

    // Event triggered when the toast is hidden.
    public event Action? OnHide;

    private System.Timers.Timer? _autoHideTimer;

    /// <summary>
    /// Auto hide timer interval in milliseconds. 
    /// Specifies the duration the toast will remain visible before being hidden.
    /// </summary>
    private const int AutoHideInterval = 4000;

    /// <summary>
    /// Initializes a new instance of the <see cref="ToastNotificationService"/> class.
    /// </summary>
    public ToastNotificationService()
    {
        InitializeTimer(AutoHideInterval);
    }

    /// <summary>
    /// Dispose method to clean up the timer and other resources when the ToastService is no longer needed.
    /// </summary>
    public void Dispose()
    {
        _autoHideTimer?.Dispose();
    }

    /// <summary>
    /// Shows a toast notification with the provided message, title, and type.
    /// It triggers the OnShow event and starts the timer for auto-hiding the toast.
    /// </summary>
    /// <param name="message">The message to display in the toast.</param>
    /// <param name="title">The title of the toast notification.</param>
    /// <param name="type">The type of the toast (e.g., success, error, info, etc.).</param>
    public void ShowToast(string message, string title, ToastNotificationType type)
    {
        OnShow?.Invoke(message, title, type);
        StartTimer();
    }

    private void StartTimer()
    {
        if (_autoHideTimer!.Enabled)
        {
            _autoHideTimer.Stop();
        }

        _autoHideTimer.Start();
    }

    private void InitializeTimer(int interval)
    {
        _autoHideTimer = new System.Timers.Timer(interval);
        _autoHideTimer.Elapsed += (_, _) => HideToast();
        _autoHideTimer.AutoReset = false;
    }

    private void HideToast()
    {
        OnHide?.Invoke();
    }
}
