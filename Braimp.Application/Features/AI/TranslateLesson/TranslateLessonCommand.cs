using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.TranslateLesson;
public class TranslateLessonCommand : IRequest<AiMessage>
{
    public string Language { get; set; } = string.Empty;
    public Guid LessonId { get; set; }
}
