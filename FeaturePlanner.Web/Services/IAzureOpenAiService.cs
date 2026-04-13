namespace FeaturePlanner.Web.Services;

/// <summary>
/// Service interface for interacting with Azure OpenAI API.
/// Handles chat completions with streaming support.
/// </summary>
public interface IAzureOpenAiService
{
    /// <summary>
    /// Sends a chat message and returns a streaming response.
    /// </summary>
    /// <param name="messages">List of chat messages in conversation history</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>Async enumerable of response text chunks for streaming</returns>
    IAsyncEnumerable<string> GetChatCompletionStreamAsync(
        IEnumerable<ChatMessage> messages,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents a single message in the chat conversation.
/// </summary>
public class ChatMessage
{
    public required string Role { get; set; } // "user", "assistant", "system"
    public required string Content { get; set; }
}
