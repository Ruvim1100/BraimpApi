using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
public class PublishedModuleLookupModule : IMapWith<Module>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SortIndex { get; set; }
    public int LessonCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Module, PublishedModuleLookupModule>()
            .ForMember(dest => dest.LessonCount, opt =>
                opt.MapFrom(src => src.Lessons.Count(lesson => lesson.IsPublished)));
    }
}
