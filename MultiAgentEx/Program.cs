using Microsoft.Extensions.AI;
using MultiAgentEx.Services;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Read OpenAI configuration.
string apiKey =
    builder.Configuration["OpenAI:OpenAIKey"]
    ?? throw new InvalidOperationException(
        "OpenAI API key is not configured.");

string model =
    builder.Configuration["OpenAI:Model"]
    ?? "gpt-5.4-mini";

// Register OpenAI as Microsoft.Extensions.AI IChatClient.
IChatClient chatClient = new OpenAIClient(apiKey)
    .GetChatClient(model)
    .AsIChatClient();

builder.Services.AddSingleton(chatClient);


builder.Services.AddScoped<AgentFactory>();
builder.Services.AddScoped<AgentOutputParser>();
builder.Services.AddScoped<AgentFactory>();
builder.Services.AddScoped<HandoffWorkflowFactory>();
builder.Services.AddScoped<HandoffOrchestratorService>();
builder.Services.AddScoped<AgentResultValidator>();
//added new service
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
