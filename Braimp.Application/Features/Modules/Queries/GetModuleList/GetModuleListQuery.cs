using MediatR;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class GetModuleListQuery : IRequest<ModuleListResponse>
{
    public Guid CourseId { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsVisibleToStudent { get; set; }
}
