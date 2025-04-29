using Braimp.Application.Common.Dtos;
using Braimp.Application.Constants;
using Microsoft.SemanticKernel;

namespace Braimp.Infrastructure.Ai.Skills;
internal class SummarizeSkill(Kernel kernel)
{
    public async Task<SummarizeLessonResponse> RunAsync(SummarizeLessonRequest request, CancellationToken cancellationToken)
    {
        var prompt = string.Format(PromptTemplates.Summarize, request.Content);
        var result = await kernel.InvokePromptAsync(prompt, arguments: null, cancellationToken: cancellationToken);
        return new SummarizeLessonResponse(result.GetValue<string>() ?? string.Empty);
    }
}
