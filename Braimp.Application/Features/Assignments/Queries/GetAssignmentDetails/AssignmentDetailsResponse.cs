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
    public Guid CourseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Assignment, AssignmentDetailsResponse>();
    }
}
