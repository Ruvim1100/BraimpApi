namespace Braimp.Application.Features.Lessons.Queries.GetPublishedLessonList;
public class PublishedLessonListResponse
{
    public IList<PublishedLessonLookupModel> Lessons { get; set; } 
        = new List<PublishedLessonLookupModel>();
}
