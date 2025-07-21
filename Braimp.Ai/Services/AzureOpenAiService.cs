using Braimp.Application.Abstraction;
using Braimp.Infrastructure.Ai.Skills;
using Microsoft.SemanticKernel;
using Braimp.Application.Modules;
using Braimp.Ai.Option;
using Microsoft.Extensions.Options;
using Braimp.Ai.Skills;

namespace Braimp.Ai.Services;
public class AzureOpenAiService : IAiService
{
    private readonly SummarizeSkill _summarizeLesson;
    private readonly GenerateTestSkill _generateTest;
    private readonly TranslateSkills _translateLesson;

    public AzureOpenAiService(IOptions<AiOptions> options)
    {
        var endpoint = options.Value.Endpoint;
        var apiKey = options.Value.ApiKey;
        var deployment = options.Value.DeploymentName;

        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(deployment, endpoint, apiKey);
        var kernel = builder.Build();

        _summarizeLesson = new SummarizeSkill(kernel);
        _generateTest = new GenerateTestSkill(kernel);
        _translateLesson = new TranslateSkills(kernel);
    }

    public Task<AiMessage> SummarizeLessonAsync(string promt, CancellationToken cancellationToken = default) =>
        _summarizeLesson.RunAsync(promt, cancellationToken);

    public Task<AiMessage> GenerateTestAsync(string promt, CancellationToken cancellationToken = default) =>
        _generateTest.RunAsync(promt, cancellationToken);

    public Task<AiMessage> TranslateLessonAsync(string promt, CancellationToken cancellationToken = default) =>
        _translateLesson.RunAsync(promt, cancellationToken);
}
