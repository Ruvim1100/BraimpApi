using Braimp.Application.Abstraction;
using Braimp.Infrastructure.Ai.Skills;
using Microsoft.SemanticKernel;
using Braimp.Application.Modules;
using Braimp.Ai.Option;
using Microsoft.Extensions.Options;

namespace Braimp.Infrastructure.Ai.Services;
public class AzureOpenAiService : IAiService
{
    private readonly SummarizeSkill _summarize;
    private readonly GenerateTestSkill _generateTest;

    public AzureOpenAiService(IOptions<AiOptions> options)
    {
        var endpoint = options.Value.Endpoint;
        var apiKey = options.Value.ApiKey;
        var deployment = options.Value.DeploymentName;

        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey);
        var kernel = builder.Build();

        _summarize = new SummarizeSkill(kernel);
        _generateTest = new GenerateTestSkill(kernel);
    }

    public Task<AiMessage> SummarizeLessonAsync(AiMessage request, CancellationToken cancellationToken = default) =>
        _summarize.RunAsync(request, cancellationToken);

    public Task<AiMessage> GenerateTestAsync(AiMessage request, CancellationToken cancellationToken = default) =>
        _generateTest.RunAsync(request, cancellationToken);
}
