using Microsoft.Agents.AI.Workflows;

namespace MultiAgentEx.Services
{
    public class HandoffWorkflowFactory
    {
        private readonly AgentFactory _agentFactory;

        public HandoffWorkflowFactory(AgentFactory agentFactory)
        {
            _agentFactory = agentFactory;
        }

        public Workflow Create()
        {
            // Create all participating agents.
            var triageAgent = _agentFactory.CreateTriageAgent();

            var dotNetAgent = _agentFactory.CreateDotNetAgent();

            var databaseAgent = _agentFactory.CreateDatabaseAgent();

            var azureAgent = _agentFactory.CreateAzureAgent();

            // Define the permitted handoff routes.
            var workflow = AgentWorkflowBuilder
                .CreateHandoffBuilderWith(triageAgent)

                // Triage can hand off to any specialist.
                .WithHandoffs(
                    triageAgent,
                    [dotNetAgent, databaseAgent, azureAgent])

                // Specialists can hand off back to triage.
                .WithHandoffs(
                    [dotNetAgent, databaseAgent, azureAgent],
                    triageAgent)

                // Allow the workflow to continue without waiting
                // for a new user message after each agent response.
                .WithAutonomousMode(
                    turnLimit: 3,
                    continuationPrompt:
                        "Continue the task using the available conversation context.")

                // Stop once triage produces its final answer.
                .WithTerminationCondition(conversation =>
                    conversation.Any(message =>
                        message.Text?.Contains(
                            "FINAL_ANSWER:",
                            StringComparison.OrdinalIgnoreCase) == true))

                .Build();

            return workflow;
        }
    }
}