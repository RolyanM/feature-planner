using Microsoft.AspNetCore.Components;

namespace FeaturePlanner.Web.Components;

/// <summary>
/// Main layout component for the Feature Planner chat interface.
/// Provides a responsive two-column layout with sidebar navigation and main chat area.
/// </summary>
public partial class ChatLayout : ComponentBase
{
    private ToastNotification? ToastRef { get; set; }

    private List<string> DummySessions { get; } = new()
    {
        "Epic: Payment Gateway",
        "Feature: User Authentication",
        "Story: Dashboard Analytics",
        "Epic: Mobile Support"
    };

    /// <summary>
    /// Handles clicking on a session in the sidebar.
    /// Shows a "coming soon" toast notification.
    /// </summary>
    private void HandleSessionClick(string sessionName)
    {
        ToastRef?.Show($"Coming soon: {sessionName}");
    }
}
