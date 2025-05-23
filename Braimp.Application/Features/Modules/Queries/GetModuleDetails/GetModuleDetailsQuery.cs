using MediatR;

namespace Braimp.Application.Features.Modules.Queries.GetModuleDetails;
public class GetModuleDetailsQuery : IRequest<ModuleDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
