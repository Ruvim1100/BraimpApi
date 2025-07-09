using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
public class LessonBlockLookupModel : IMapWith<LessonBlock>
{
    public Guid Id { get; set; }
    public string BlockType { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LessonBlock, LessonBlockLookupModel>();
    }
}
