using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Assignments;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class AssignmentDetailsResponse : IMapWith<Assignment>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public ICollection<AssignmentFileModel> AssignmentFiles { get; set; } = new List<AssignmentFileModel>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Assignment, AssignmentDetailsResponse>()
               .ForMember(dest => dest.AssignmentFiles, opt => opt.Ignore());
    }
}
