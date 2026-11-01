namespace MultiAgentEx.Models
{
    public class AnalysisResult
    {
        public string Summary { get; set; } = string.Empty;

        public List<AgentFinding> Findings { get; set; } = [];

        public List<string> Recommendations { get; set; } = [];

        public List<string> Limitations { get; set; } = [];

    }
}
