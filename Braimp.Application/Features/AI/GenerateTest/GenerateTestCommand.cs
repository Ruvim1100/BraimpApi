using MediatR;

namespace Braimp.Application.Features.AI.GenerateTest;
public class GenerateTestCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public string Language { get; set; } = string.Empty;
    public string SourceText { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}