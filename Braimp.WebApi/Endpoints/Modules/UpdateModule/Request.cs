using AutoMapper;
using Braimp.Application.Features.Modules.Commands.UpdateModule;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Modules.UpdateModule;
public class Request : IMapWith<UpdateModuleCommand>
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool? IsPublished { get; set; }
    public int? SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateModuleCommand>();
    }
}
