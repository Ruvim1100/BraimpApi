using Braimp.Application.Modules;

namespace Braimp.Application.Abstraction;
public interface IAiService
{
    Task<AiMessage> SummarizeLessonAsync(AiMessage request, CancellationToken cancellationToken = default);
    Task<AiMessage> GenerateTestAsync(string promt, CancellationToken cancellationToken = default);
    Task<AiMessage> TranslateLessonAsync(string promt, CancellationToken cancellationToken = default);
}
