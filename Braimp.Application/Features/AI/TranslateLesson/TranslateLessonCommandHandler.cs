using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.TranslateLesson;
public class TranslateLessonCommandHandler(IAiService aiService) : IRequestHandler<TranslateLessonCommand, AiMessage>
{
    public async Task<AiMessage> Handle(TranslateLessonCommand request, CancellationToken cancellationToken)
    {
        var promt = string.Format(PromptTemplates.TranslateLesson, request.Language, request.SourceText);
        return await aiService.TranslateLessonAsync(promt, cancellationToken);
    }
}   
