using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Modules.Commands.UpdateModule;

namespace Braimp.WebApi.Endpoints.Modules.UpdateModule;
public class UpdateModuleDto : IMapWith<UpdateModuleCommand>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsVisibleToStudent { get; set; }
    public int? SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateModuleDto, UpdateModuleCommand>();
    }
}
