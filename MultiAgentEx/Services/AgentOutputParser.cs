using System.Text.Json;
using System.Text.Json.Serialization;

namespace MultiAgentEx.Services
{
    public sealed class AgentOutputParser
    {
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            UnmappedMemberHandling =JsonUnmappedMemberHandling.Disallow
        };

        public T Parse<T>(string output) where T : class
        {
            if (string.IsNullOrWhiteSpace(output))
            {
                throw new InvalidOperationException(
                    "The agent returned an empty response.");
            }

            try
            {
                var result = JsonSerializer.Deserialize<T>(
                    output,
                    _options);

                if (result is null)
                {
                    throw new InvalidOperationException(
                        "The agent returned a null result.");
                }

                return result;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException(
                    $"The agent returned invalid JSON for {typeof(T).Name}.",
                    ex);
            }
        }
    }
}
