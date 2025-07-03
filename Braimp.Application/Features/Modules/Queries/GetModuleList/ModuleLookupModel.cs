using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class ModuleLookupModel : IMapWith<Module>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public int SortIndex { get; set; }
    public List<LessonLookupModel> Lessons { get; set; }
        = new List<LessonLookupModel>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Module, ModuleLookupModel>();
        profile.CreateMap<Module, ModuleLookupModel>()
            .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons.OrderBy(lesson => lesson.SortIndex)));

        profile.CreateMap<Lesson, LessonLookupModel>();
    }
}
