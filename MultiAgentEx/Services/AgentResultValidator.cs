using MultiAgentEx.Models;

namespace MultiAgentEx.Services
{
    public sealed class AgentResultValidator
    {
        public void ValidateResearch(ResearchResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            if (string.IsNullOrWhiteSpace(result.Summary))
            {
                throw new InvalidOperationException(
                    "Research result must contain a summary.");
            }

            if (result.Findings is null)
            {
                throw new InvalidOperationException(
                    "Research findings cannot be null.");
            }

            if (result.MissingInformation is null)
            {
                throw new InvalidOperationException(
                    "MissingInformation cannot be null.");
            }

            foreach (var finding in result.Findings)
            {
                ValidateFinding(finding, "Research");
            }

            if (result.MissingInformation.Any(
                string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(
                    "Research contains an empty missing-information item.");
            }
        }

        public void ValidateAnalysis(AnalysisResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            if (string.IsNullOrWhiteSpace(result.Summary))
            {
                throw new InvalidOperationException(
                    "Analysis result must contain a summary.");
            }

            if (result.Findings is null ||
                result.Recommendations is null ||
                result.Limitations is null)
            {
                throw new InvalidOperationException(
                    "Analysis result contains a null collection.");
            }

            foreach (var finding in result.Findings)
            {
                ValidateFinding(finding, "Analysis");
            }

            if (result.Recommendations.Any(
                string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(
                    "Analysis contains an empty recommendation.");
            }

            if (result.Limitations.Any(
                string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(
                    "Analysis contains an empty limitation.");
            }
        }

        public void ValidateReview(ReviewResult result)
        {
            ArgumentNullException.ThrowIfNull(result);

            if (string.IsNullOrWhiteSpace(
                result.ExecutiveSummary))
            {
                throw new InvalidOperationException(
                    "The final review must contain an executive summary.");
            }

            if (result.KeyFindings is null ||
                result.RecommendedActions is null ||
                result.AdditionalInformationNeeded is null ||
                result.ReviewWarnings is null)
            {
                throw new InvalidOperationException(
                    "The final review contains a null collection.");
            }

            foreach (var finding in result.KeyFindings)
            {
                ValidateFinding(finding, "Review");
            }

            if (result.RecommendedActions.Any(
                string.IsNullOrWhiteSpace))
            {
                throw new InvalidOperationException(
                    "The final review contains an empty recommendation.");
            }
        }

        private static void ValidateFinding(
            AgentFinding? finding,
            string agentName)
        {
            if (finding is null)
            {
                throw new InvalidOperationException(
                    $"{agentName} contains a null finding.");
            }

            if (string.IsNullOrWhiteSpace(finding.Title))
            {
                throw new InvalidOperationException(
                    $"{agentName} contains a finding without a title.");
            }

            if (string.IsNullOrWhiteSpace(finding.Description))
            {
                throw new InvalidOperationException(
                    $"{agentName} contains a finding without a description.");
            }

            if (string.IsNullOrWhiteSpace(finding.Evidence))
            {
                throw new InvalidOperationException(
                    $"{agentName} contains a finding without an evidence description.");
            }

            string[] validConfidenceLevels =
            [
                "Low",
            "Medium",
            "High",
            "Unknown"
            ];

            if (!validConfidenceLevels.Contains(
                finding.Confidence,
                StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"{agentName} contains an invalid confidence value.");
            }
        }
    }
}