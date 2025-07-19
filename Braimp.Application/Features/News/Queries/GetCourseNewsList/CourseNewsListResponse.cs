namespace Braimp.Application.Features.News.Queries.GetCourseNewsList;
public class CourseNewsListResponse
{
    public List<CourseNewsLookupModel> CourseNews { get; set; } = new List<CourseNewsLookupModel>();
}
