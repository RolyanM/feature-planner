using Microsoft.AspNetCore.Components;

namespace FeaturePlanner.Web.Components;

/// <summary>
/// Plan summary component displaying key Q&A pairs from the interview.
/// Allows user to review and approve before proceeding to generation.
/// </summary>
public partial class PlanSummary : ComponentBase
{
    /// <summary>
    /// The conversation messages from the interview phase.
    /// </summary>
    [Parameter]
    public List<ConversationMessage>? ConversationMessages { get; set; }

    /// <summary>
    /// Callback when user clicks "Back to Interview".
    /// </summary>
    [Parameter]
    public EventCallback OnBack { get; set; }

    /// <summary>
    /// Callback when user clicks "Approve & Generate".
    /// </summary>
    [Parameter]
    public EventCallback OnApprove { get; set; }

    /// <summary>
    /// Represents a Q&A pair from the conversation.
    /// </summary>
    public class QAPair
    {
        public required string Question { get; set; }
        public required string Answer { get; set; }
    }

    /// <summary>
    /// Extracts Q&A pairs from the conversation messages.
    /// Pairs each assistant message with the following user message.
    /// </summary>
    private List<QAPair> GetQAPairs()
    {
        var pairs = new List<QAPair>();

        if (ConversationMessages == null || ConversationMessages.Count < 2)
            return pairs;

        for (int i = 0; i < ConversationMessages.Count - 1; i++)
        {
            var current = ConversationMessages[i];
            var next = ConversationMessages[i + 1];

            // If current is assistant question and next is user answer, create a pair
            if (current.Role == "assistant" && next.Role == "user")
            {
                pairs.Add(new QAPair
                {
                    Question = current.Content,
                    Answer = next.Content
                });

                i++; // Skip the next message since we've paired it
            }
        }

        return pairs;
    }
}
