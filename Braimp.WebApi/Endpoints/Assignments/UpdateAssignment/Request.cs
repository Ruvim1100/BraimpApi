using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Assignments.UpdateAssignment;
public class Request : IMapWith<UpdateAssignmentCommand>
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTimeOffset? Deadline { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateAssignmentCommand>();
    }
}
