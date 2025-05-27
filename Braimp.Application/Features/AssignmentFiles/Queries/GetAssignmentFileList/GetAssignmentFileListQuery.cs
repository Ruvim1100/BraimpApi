using MediatR;

namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class GetAssignmentFileListQuery : IRequest<AssignmentFileListResponse>
{
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
