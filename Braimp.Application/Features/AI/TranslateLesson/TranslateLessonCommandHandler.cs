using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Application.Modules;
using Braimp.Domain.Entities.LearningContent.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AI.TranslateLesson;
public class TranslateLessonCommandHandler(IBraimpDbContext dbContext,IAiService aiService) : IRequestHandler<TranslateLessonCommand, AiMessage>
{
    public async Task<AiMessage> Handle(TranslateLessonCommand request, CancellationToken cancellationToken)
    {
        var textBlocks = await dbContext.LessonBlocks
            .Where(block => block.LessonId == request.LessonId && 
            block.BlockType == LessonBlockType.Text)
            .ToListAsync(cancellationToken);

        if (textBlocks.Count == 0)
        {
            return new AiMessage("No text blocks available to translate.");
        }

        var combinedText = string.Join("\n\n", textBlocks.Select(block => block.Content));

        var promt = string.Format(PromptTemplates.TranslateLesson, request.Language, combinedText);
        return await aiService.TranslateLessonAsync(promt, cancellationToken);
    }
}   
