namespace MultiAgentEx.Models
{
    public sealed class ResearchFinding
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Evidence { get; set; } = string.Empty;

        public string Confidence { get; set; } = "Unknown";
    }

}
