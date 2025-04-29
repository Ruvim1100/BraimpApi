using MediatR;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsVisibleToStudent { get; set; }
    public int SortIndex { get; set; }
    public Guid CourseId { get; set; }
}
