using MediatR;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string? Title { get; set; }
    public bool? IsPublished { get; set; }
    public int? SortIndex { get; set; }
}
