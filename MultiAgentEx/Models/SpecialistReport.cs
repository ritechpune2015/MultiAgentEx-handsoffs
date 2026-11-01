namespace MultiAgentEx.Models
{
    public class SpecialistReport
    {
        public string AgentName { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public List<AgentFinding> Findings { get; set; } = [];

        public List<string> Recommendations { get; set; } = [];

        public List<string> Limitations { get; set; } = [];

    }
}
