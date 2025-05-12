using Braimp.Application.Abstraction;
using MediatR;
using Braimp.Application.Modules;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public class SummarizeLessonCommandHandler(IAiService aiService) : IRequestHandler<SummarizeLessonCommand, AiMessage>
{
    public async Task<AiMessage> Handle(SummarizeLessonCommand request, CancellationToken cancellationToken)
    {
        var summarizeLessonResponse = new AiMessage(request.Content);
        return await aiService.SummarizeLessonAsync(summarizeLessonResponse, cancellationToken);
    }
}
