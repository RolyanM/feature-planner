using Microsoft.AspNetCore.Components;

namespace FeaturePlanner.Web.Components;

/// <summary>
/// Toast notification component displaying temporary messages to the user.
/// Auto-dismisses after a configurable duration (default 3 seconds).
/// </summary>
public partial class ToastNotification : ComponentBase, IAsyncDisposable
{
    private bool IsVisible { get; set; }
    private string Message { get; set; } = string.Empty;
    private System.Timers.Timer? _dismissTimer;
    private const int DismissDelayMs = 3000; // 3 seconds

    /// <summary>
    /// Shows a toast message that auto-dismisses after DismissDelayMs.
    /// </summary>
    /// <param name="message">The message to display</param>
    public void Show(string message)
    {
        Message = message;
        IsVisible = true;
        StateHasChanged();

        // Cancel any existing timer
        _dismissTimer?.Stop();
        _dismissTimer?.Dispose();

        // Create a new timer to auto-dismiss
        _dismissTimer = new System.Timers.Timer(DismissDelayMs);
        _dismissTimer.Elapsed += (s, e) =>
        {
            Hide();
        };
        _dismissTimer.AutoReset = false;
        _dismissTimer.Start();
    }

    /// <summary>
    /// Hides the toast notification.
    /// </summary>
    public void Hide()
    {
        IsVisible = false;
        StateHasChanged();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        _dismissTimer?.Stop();
        _dismissTimer?.Dispose();
        await Task.CompletedTask;
    }
}
