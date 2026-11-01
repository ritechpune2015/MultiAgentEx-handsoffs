namespace MultiAgentEx.Models
{
    public sealed class MultiAgentResult
    {
        //public string ResearchReport { get; set; } = string.Empty;

        //public string AnalysisReport { get; set; } = string.Empty;

        //public string FinalAnswer { get; set; } = string.Empty;

        //public TimeSpan Duration { get; set; }

        public ResearchResult Research { get; set; } = new();
        public AnalysisResult Analysis { get; set; } = new();
        public ReviewResult Review { get; set; } = new();
        public TimeSpan Duration { get; set; }

    }

}
