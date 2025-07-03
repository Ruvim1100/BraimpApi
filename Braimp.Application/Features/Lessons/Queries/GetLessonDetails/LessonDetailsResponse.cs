using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class LessonDetailsResponse : IMapWith<Lesson>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublished { get; set; }
    public int SortIndex { get; set; }
    public List<FileResourceModel> Files { get; set; } = new List<FileResourceModel>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Lesson, LessonDetailsResponse>();
    }
}
