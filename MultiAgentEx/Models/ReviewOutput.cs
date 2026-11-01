namespace MultiAgentEx.Models
{
    public sealed class ReviewOutput
    {
        public string ExecutiveSummary { get; set; } =
            string.Empty;

        public List<string> FinalRecommendations { get; set; } = [];

        public List<string> ReviewWarnings { get; set; } = [];
    }
}
