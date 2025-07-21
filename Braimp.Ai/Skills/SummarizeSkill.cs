using Braimp.Application.Modules;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Skills;
internal class SummarizeSkill(Kernel kernel)
{
    public async Task<AiMessage> RunAsync(string promt, CancellationToken cancellationToken)
    {
        var result = await kernel.InvokePromptAsync(promt, arguments: null, cancellationToken: cancellationToken);
        return new AiMessage(result.GetValue<string>() ?? string.Empty);
    }
}
