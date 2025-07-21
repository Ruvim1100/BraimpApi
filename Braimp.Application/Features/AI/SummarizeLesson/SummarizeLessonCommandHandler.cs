using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Application.Modules;
using Braimp.Domain.Entities.LearningContent.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public class SummarizeLessonCommandHandler(IBraimpDbContext dbContext, IAiService aiService) 
    : IRequestHandler<SummarizeLessonCommand, AiMessage>
{
    public async Task<AiMessage> Handle(SummarizeLessonCommand request, CancellationToken cancellationToken)
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

        var promt = string.Format(PromptTemplates.SummarizeLesson, combinedText);
        return await aiService.SummarizeLessonAsync(promt, cancellationToken);
    }
}
