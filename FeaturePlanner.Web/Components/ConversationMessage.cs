namespace FeaturePlanner.Web.Components;

/// <summary>
/// Represents a single message in the chat conversation.
/// </summary>
public class ConversationMessage
{
    public required string Role { get; set; } // "user" or "assistant"
    public required string Content { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public bool IsUserMessage => Role == "user";
}
