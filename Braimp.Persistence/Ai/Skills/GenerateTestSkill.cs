using Braimp.Application.Common.Dtos;
using Braimp.Application.Constants;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Skills;
public class GenerateTestSkill(Kernel kernel)
{
    public async Task<GenerateTestResponse> RunAsync(GenerateTestRequest request, CancellationToken cancellationToken)
    {
        var prompt = string.Format(PromptTemplates.GenerateTest, request.Content);
        var result = await kernel.InvokePromptAsync(prompt, arguments: null, cancellationToken: cancellationToken);
        return new GenerateTestResponse(result.GetValue<string>() ?? string.Empty);
    }
}
