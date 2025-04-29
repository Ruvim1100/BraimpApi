using Braimp.Application.Common.Dtos;

namespace Braimp.Application.Abstraction;
public interface IAiService
{
    Task<SummarizeLessonResponse> SummarizeLessonAsync(SummarizeLessonRequest request, CancellationToken cancellationToken = default);
    Task<GenerateTestResponse> GenerateTestAsync(GenerateTestRequest request, CancellationToken cancellationToken = default);
}
