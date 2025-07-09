using MediatR;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public Guid CourseId { get; set; }
}
