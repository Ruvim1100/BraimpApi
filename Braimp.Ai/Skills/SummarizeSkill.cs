using Braimp.Application.Constants;
using Braimp.Application.Modules;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Skills;
internal class SummarizeSkill(Kernel kernel)
{
    public async Task<AiMessage> RunAsync(AiMessage request, CancellationToken cancellationToken)
    {
        var prompt = string.Format(PromptTemplates.Summarize, request.message);
        var result = await kernel.InvokePromptAsync(prompt, cancellationToken: cancellationToken);
        return new AiMessage(result.GetValue<string>() ?? string.Empty);
    }
}
