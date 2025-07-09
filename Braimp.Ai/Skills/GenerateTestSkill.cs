using Braimp.Application.Modules;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Skills;
public class GenerateTestSkill(Kernel kernel)
{
    public async Task<AiMessage> RunAsync(string prompt, CancellationToken cancellationToken)
    {
        var result = await kernel.InvokePromptAsync(prompt, arguments: null, cancellationToken: cancellationToken);
        return new AiMessage(result.GetValue<string>() ?? string.Empty);
    }
}
