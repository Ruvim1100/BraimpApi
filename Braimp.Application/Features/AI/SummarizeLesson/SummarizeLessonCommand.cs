using Braimp.Application.Common.Dtos;
using MediatR;

namespace Braimp.Application.Features.AI.SummarizeLesson;
public record class SummarizeLessonCommand(string Content) : IRequest<SummarizeLessonResponse>;

