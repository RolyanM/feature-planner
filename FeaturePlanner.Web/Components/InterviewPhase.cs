namespace FeaturePlanner.Web.Components;

/// <summary>
/// Represents the current view/phase of the interview workflow.
/// </summary>
public enum InterviewPhase
{
    /// <summary>Interactive chat interview with AI coach</summary>
    Interview,
    
    /// <summary>Review planned decisions before generation</summary>
    PlanSummary,
    
    /// <summary>Display generated Epic/Features/Stories for editing</summary>
    Generation
}
