using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Modules.Commands.CreateModule;

namespace Braimp.WebApi.Endpoints.Modules.CreateModule;
public class CreateModuleDto : IMapWith<CreateModuleCommand>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsVisibleToStudent { get; set; }
    public int SortIndex { get; set; }
    public Guid CourseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateModuleDto, CreateModuleCommand>();
    }
}
