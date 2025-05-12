using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class ModuleLookupDto : IMapWith<Module>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublished { get; set; }
    public int SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Module, ModuleLookupDto>();
    }
}
