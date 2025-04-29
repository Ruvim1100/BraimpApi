using Braimp.Application.Common.Dtos;
using Braimp.Application.Abstraction;
using Braimp.Infrastructure.Ai.Skills;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Services;
public class AzureOpenAiService : IAiService
{
    private readonly SummarizeSkill _summarize;
    private readonly GenerateTestSkill _generateTest;

    public AzureOpenAiService(IConfiguration config)
    {
        var endpoint = config["AzureOpenAi:Endpoint"]!;
        var apiKey = config["AzureOpenAi:ApiKey"]!;
        var deployment = config["AzureOpenAi:DeploymentName"]!;

        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey);
        var kernel = builder.Build();

        _summarize = new SummarizeSkill(kernel);
        _generateTest = new GenerateTestSkill(kernel);
    }

    public Task<SummarizeLessonResponse> SummarizeLessonAsync(SummarizeLessonRequest request, CancellationToken cancellationToken = default) =>
        _summarize.RunAsync(request, cancellationToken);

    public Task<GenerateTestResponse> GenerateTestAsync(GenerateTestRequest request, CancellationToken cancellationToken = default) =>
        _generateTest.RunAsync(request, cancellationToken);
}
