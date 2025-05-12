using MediatR;

namespace Braimp.Application.Features.Modules.Commands.DeleteModule;
public class DeleteModuleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}

