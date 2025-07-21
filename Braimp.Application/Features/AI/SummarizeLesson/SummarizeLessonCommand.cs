using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public class SummarizeLessonCommand : IRequest<AiMessage>
{
    public Guid LessonId { get; set; }
}

