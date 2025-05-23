using AutoMapper;
using Braimp.Application.Features.Modules.Commands.UpdateModule;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Modules.UpdateModule;
public class Request : IMapWith<UpdateModuleCommand>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsPublished { get; set; }
    public int? SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateModuleCommand>();
    }
}
