namespace MultiAgentEx.Models
{
    public class ReviewResult
    {
        public string ExecutiveSummary { get; set; } = string.Empty;

        public List<AgentFinding> KeyFindings { get; set; } = [];

        public List<string> RecommendedActions { get; set; } = [];

        public List<string> AdditionalInformationNeeded { get; set; } = [];

        public List<string> ReviewWarnings { get; set; } = [];

    }
}
