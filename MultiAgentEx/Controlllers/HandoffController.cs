using Microsoft.AspNetCore.Mvc;
using MultiAgentEx.Models;
using MultiAgentEx.Services;

namespace MultiAgentEx.Controlllers
{
    public sealed class HandoffController : ControllerBase
    {
        private readonly HandoffOrchestratorService _orchestrator;

        private readonly ILogger<HandoffController> _logger;

        public HandoffController(HandoffOrchestratorService orchestrator, ILogger<HandoffController> logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] HandoffRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _orchestrator.RunAsync(
                    request.Question,
                    cancellationToken);

                var result = messages.Select(message => new
                {
                    message.Role,
                    message.AuthorName,
                    message.Text
                });

                return Ok(new
                {
                    Question = request.Question,
                    Conversation = result
                });
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                return StatusCode(499, new
                {
                    Message = "Request cancelled."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Handoff workflow failed.");

                return Problem(
                    title: "Unable to process the technical question.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}