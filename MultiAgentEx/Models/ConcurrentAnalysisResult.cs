namespace MultiAgentEx.Models
{
    public class ConcurrentAnalysisResult
    {
        public string Question { get; set; } = string.Empty;

        public List<SpecialistReport> SpecialistReports { get; set; } = [];

        public string ExecutiveSummary { get; set; } = string.Empty;

        public List<string> FinalRecommendations { get; set; } = [];

        public List<string> ReviewWarnings { get; set; } = [];

        public Dictionary<string, double> AgentDurationsMs { get; set; } = [];

        public double TotalDurationMs { get; set; }
    }

}
