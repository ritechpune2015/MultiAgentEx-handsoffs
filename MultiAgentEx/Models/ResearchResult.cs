namespace MultiAgentEx.Models
{
    public sealed class ResearchResult
    {
        //public string Summary { get; set; } = string.Empty;

        //public List<ResearchFinding> Findings { get; set; } = [];

        //public List<string> MissingInformation { get; set; } = [];

        public string Summary { get; set; } = string.Empty;

        public List<AgentFinding> Findings { get; set; } = [];

        public List<string> MissingInformation { get; set; } = [];

    }

}
