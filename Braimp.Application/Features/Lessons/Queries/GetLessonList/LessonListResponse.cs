namespace Braimp.Application.Features.Lessons.Queries.GetLessonList;
public class LessonListResponse
{
    public IList<LessonLookupModel> Lessons { get; set; } = new List<LessonLookupModel>();
}
