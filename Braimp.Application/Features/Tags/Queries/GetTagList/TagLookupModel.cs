using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Tags;

namespace Braimp.Application.Features.Tags.Queries.GetTagList;
public class TagLookupModel : IMapWith<Tag>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagLookupModel>();
    }
}
