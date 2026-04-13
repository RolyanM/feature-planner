using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FeaturePlanner.Web.Components;

/// <summary>
/// Main layout component for the Feature Planner chat interface.
/// Provides a responsive two-column layout with sidebar navigation and main chat area.
/// Manages the interview conversation between user and agile coach AI.
/// Handles transitions between interview and plan summary phases.
/// </summary>
public partial class ChatLayout : ComponentBase
{
    private ToastNotification? ToastRef { get; set; }
    private ElementReference MessagesContainerRef { get; set; }

    // Phase management
    private InterviewPhase CurrentPhase { get; set; } = InterviewPhase.Interview;

    // Chat state
    private List<ConversationMessage> Messages { get; } = new();
    private string InputValue { get; set; } = string.Empty;
    private bool IsLoading { get; set; }

    private List<string> DummySessions { get; } = new()
    {
        "Epic: Payment Gateway",
        "Feature: User Authentication",
        "Story: Dashboard Analytics",
        "Epic: Mobile Support"
    };

    // Agile coach system prompt
    private const string AgileCoachSystemPrompt = @"You are an experienced agile coach and product management expert. Your role is to conduct a structured interview with a product owner or PM to deeply understand their feature or epic ideas.

Interview Guidelines:
- Ask ONE question at a time, never multiple questions
- Ask clarifying questions about the goal, users, and success metrics
- Help them think through edge cases and error scenarios
- Ensure they've considered authentication, permissions, and data model
- Guide them toward a clear, actionable description
- Be conversational but professional
- When they've provided enough information, summarize what you've learned and ask if you should proceed to generate issues

Start by asking about the overall goal of their feature/epic.";

    /// <summary>
    /// Handles clicking on a session in the sidebar.
    /// Shows a 'coming soon' toast notification.
    /// </summary>
    private void HandleSessionClick(string sessionName)
    {
        ToastRef?.Show($"Coming soon: {sessionName}");
    }

    /// <summary>
    /// Handles Enter key press in the input field.
    /// </summary>
    private async Task HandleKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter" && !IsLoading && !string.IsNullOrWhiteSpace(InputValue))
        {
            await HandleSend();
        }
    }

    /// <summary>
    /// Sends the user's message and gets a response from the AI coach.
    /// </summary>
    private async Task HandleSend()
    {
        if (string.IsNullOrWhiteSpace(InputValue) || IsLoading)
            return;

        // Add user message to chat
        var userMessage = new ConversationMessage
        {
            Role = "user",
            Content = InputValue
        };
        Messages.Add(userMessage);
        InputValue = string.Empty;
        IsLoading = true;
        await InvokeAsync(StateHasChanged);

        // Simulate a slight delay before AI responds
        await Task.Delay(500);

        // For now, generate a mock response to test the UI
        var aiResponse = await GetAiCoachResponse(userMessage.Content);
        
        var assistantMessage = new ConversationMessage
        {
            Role = "assistant",
            Content = aiResponse
        };
        Messages.Add(assistantMessage);
        IsLoading = false;
        await InvokeAsync(StateHasChanged);
        await ScrollToBottom();
    }

    /// <summary>
    /// Gets a response from the AI coach with streaming simulation.
    /// For now, this returns a mock response word-by-word. Will integrate Azure OpenAI later.
    /// </summary>
    private async Task<string> GetAiCoachResponse(string userInput)
    {
        // Mock response generator - demonstrates the interview flow
        var mockResponses = new[]
        {
            "That's great context! To help me understand better, who are the primary users for this feature? What problems are they trying to solve?",
            "I see. What would success look like for this feature? How would you measure if it's working well?",
            "Excellent point. Have you thought about any edge cases or error scenarios that might come up?",
            "Thanks for sharing that. How does this feature fit into your overall product roadmap? Is it blocking other work?",
            "Those are all important considerations. It sounds like we have a solid understanding now. Should I go ahead and generate epics, features, and stories based on what we've discussed?"
        };

        // Return a different response each time (cycling through)
        var responseIndex = Messages.Count % mockResponses.Length;
        
        // Simulate streaming by building the response word-by-word
        var fullResponse = mockResponses[responseIndex];
        var words = fullResponse.Split(' ');
        var streamedResponse = new System.Text.StringBuilder();

        // For demo, show the full response immediately
        // In real implementation with Azure OpenAI, we would stream chunks here
        foreach (var word in words)
        {
            streamedResponse.Append(word).Append(' ');
            // Simulate network delay between chunks (commented out for performance)
            // await Task.Delay(50);
        }

        await Task.CompletedTask;
        return streamedResponse.ToString().Trim();
    }

    /// <summary>
    /// Scrolls the messages container to the bottom to show the latest message.
    /// </summary>
    private async Task ScrollToBottom()
    {
        try
        {
            await Task.Delay(100); // Give DOM time to update
            // Note: This requires JavaScript interop in real implementation
            // For now, just mark the operation as complete
        }
        catch
        {
            // Scroll failed - not critical
        }
    }

    /// <summary>
    /// Transitions to the plan summary phase.
    /// Shows a review of the interview conversation before generation.
    /// </summary>
    private void HandleNextReviewPlan()
    {
        CurrentPhase = InterviewPhase.PlanSummary;
        StateHasChanged();
    }

    /// <summary>
    /// Transitions back to the interview phase from plan summary.
    /// Allows user to continue the conversation.
    /// </summary>
    private void HandleBackToInterview()
    {
        CurrentPhase = InterviewPhase.Interview;
        StateHasChanged();
    }

    /// <summary>
    /// Transitions to the generation phase.
    /// User has approved the plan; ready to generate epic/features/stories.
    /// </summary>
    private void HandleApproveAndGenerate()
    {
        // TODO: Wire up Issue #7 generation phase
        CurrentPhase = InterviewPhase.Generation;
        ToastRef?.Show("Generating your epic, features, and stories...");
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        // Start the interview with an initial prompt from the AI coach
        var initialMessage = new ConversationMessage
        {
            Role = "assistant",
            Content = "Hello! I'm your agile coach, here to help you refine your feature or epic idea. Let's start by understanding your vision. What's the feature or epic you'd like to plan?"
        };
        Messages.Add(initialMessage);
        await base.OnInitializedAsync();
    }
}
