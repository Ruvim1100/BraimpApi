using Braimp.Domain.Entities.LearningContent.Enums;
using MediatR;

namespace Braimp.Application.Features.LessonBlocks.Commands.CreateLessonBlock;
public class CreateLessonBlockCommand : IRequest
{ 
    public Guid LessonId { get; set; }
    public LessonBlockType Type { get; set; }
    public string Content { get; set; } = string.Empty;
}
