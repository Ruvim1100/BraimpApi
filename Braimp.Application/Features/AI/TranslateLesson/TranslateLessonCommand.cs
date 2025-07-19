using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.TranslateLesson;
public class TranslateLessonCommand : IRequest<AiMessage>
{
    public string SourceText { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}
