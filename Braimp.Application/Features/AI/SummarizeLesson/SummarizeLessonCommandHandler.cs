using Braimp.Application.Common.Dtos;
using Braimp.Application.Abstraction;
using MediatR;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public class SummarizeLessonCommandHandler(IAiService aiService) : IRequestHandler<SummarizeLessonCommand, SummarizeLessonResponse>
{
    public async Task<SummarizeLessonResponse> Handle(SummarizeLessonCommand request, CancellationToken cancellationToken)
    {
        var summarizeLessonResponse = new SummarizeLessonRequest(request.Content);
        return await aiService.SummarizeLessonAsync(summarizeLessonResponse, cancellationToken);
    }
}
