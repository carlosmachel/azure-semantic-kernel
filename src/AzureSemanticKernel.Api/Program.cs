using Azure.AI.Agents.Persistent;
using Azure.Identity;
using AzureSemanticKernel.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Assistants;
using OpenAI.Chat;
using Scalar.AspNetCore;
using ChatMessageContent = Microsoft.SemanticKernel.ChatMessageContent;
#pragma warning disable OPENAI001

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.Configure<AzureAiAgentSettings>(builder.Configuration.GetSection("AzureAiAgentSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.MapGet("/basic-flow", async (
    [FromServices] IOptions<AzureAiAgentSettings> settings,
    string agentName, string description, string instructions) =>
{
#pragma warning disable SKEXP0110
    var client = AzureAIAgent.CreateAgentsClient(settings.Value.Uri, new DefaultAzureCredential());
    PersistentAgent definition = await client.Administration.CreateAgentAsync(settings.Value.Model, agentName, description, instructions);
    AzureAIAgent agent = new(definition, client);
    AzureAIAgentThread agentThread = new(client);
    ChatMessageContent message = new(AuthorRole.User, "Teste");
    ChatHistory history =
    [
        message

    ];

    await foreach (var response in agent.InvokeAsync(message, agentThread))
    {
        history.Add(response.Message);
    }

    return history.Select(x => x.Content);

#pragma warning restore SKEXP0110

}).WithName("basic-flow");

app.MapGet("/create-thread", async (
    [FromServices] IOptions<AzureAiAgentSettings> settings,
    string agentName, string description, string instructions) =>
{
#pragma warning disable SKEXP0110
    var client = AzureAIAgent.CreateAgentsClient(settings.Value.Uri, new DefaultAzureCredential());
    PersistentAgent agent = await client.Administration.CreateAgentAsync(settings.Value.Model, agentName, description, instructions);
#pragma warning restore SKEXP0110
    return Results.Ok(agent.Id);
}).WithName("CreateThread");

app.UseHttpsRedirection();
app.Run();