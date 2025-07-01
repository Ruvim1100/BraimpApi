using MediatR;

namespace Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
public class GetPublishedModuleListQuery : IRequest<PublishedModuleListResponse>
{
    public Guid CourseId { get; set; }
}
