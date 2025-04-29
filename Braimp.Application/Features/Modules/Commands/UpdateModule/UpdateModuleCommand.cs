using MediatR;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsVisibleToStudent { get; set; }
    public int? SortIndex { get; set; }
}
