using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Assignments.UpdateAssignment;
public class Request : IMapWith<UpdateAssignmentCommand>
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset? Deadline { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateAssignmentCommand>();
    }
}
