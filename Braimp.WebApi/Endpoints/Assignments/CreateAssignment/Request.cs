using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.CreateAssignment;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Assignments.CreateAssignment;
public class Request : IMapWith<CreateAssignmentCommand>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset Deadline { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateAssignmentCommand>();
    }
}
