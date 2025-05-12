using Braimp.Application.Modules;
using MediatR;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public record class SummarizeLessonCommand(string Content) : IRequest<AiMessage>;

