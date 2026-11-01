using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace MultiAgentEx.Services
{
    public class HandoffOrchestratorService
    {
        private readonly HandoffWorkflowFactory _workflowFactory;
        private readonly ILogger<HandoffOrchestratorService> _logger;

        public HandoffOrchestratorService(
            HandoffWorkflowFactory workflowFactory,
            ILogger<HandoffOrchestratorService> logger)
        {
            _workflowFactory = workflowFactory;
            _logger = logger;
        }

        public async Task<List<ChatMessage>> RunAsync(
            string question,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException(
                    "Question is required.",
                    nameof(question));
            }

            var workflow = _workflowFactory.Create();

            var messages = new List<ChatMessage>
        {
            new(
                ChatRole.User,
                question)
        };

            _logger.LogInformation(
                "Starting handoff workflow.");

            await using StreamingRun run =
                await InProcessExecution.RunStreamingAsync(
                    workflow,
                    messages);

            await run.TrySendMessageAsync(
                new TurnToken(emitEvents: true));

            List<ChatMessage> finalMessages = [];

            await foreach (
                WorkflowEvent evt in run.WatchStreamAsync()
                    .ConfigureAwait(false))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (evt is AgentResponseUpdateEvent update)
                {
                    _logger.LogInformation(
                        "Agent response update from {ExecutorId}",
                        update.ExecutorId);

                    // Useful when diagnosing the workflow.
                    // The final transcript is collected below.
                }
                else if (evt is WorkflowOutputEvent output)
                {
                    finalMessages =
                        output.As<List<ChatMessage>>() ?? [];

                    _logger.LogInformation(
                        "Handoff workflow completed. Message count: {Count}",
                        finalMessages.Count);

                    break;
                }
            }

            return finalMessages;
        }
    }
}