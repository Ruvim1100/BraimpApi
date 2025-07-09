namespace Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
public class LessonBlockListResponse
{
    public List<LessonBlockLookupModel> LessonBlocks { get; set; } 
        = new List<LessonBlockLookupModel>();
}
