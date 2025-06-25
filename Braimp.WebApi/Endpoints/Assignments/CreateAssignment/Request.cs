using AutoMapper;
using Braimp.Application.Features.Assignments.Commands.CreateAssignment;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Assignments.CreateAssignment;
public class Request : IMapWith<CreateAssignmentCommand>
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public DateTimeOffset Deadline { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateAssignmentCommand>();
    }
}
