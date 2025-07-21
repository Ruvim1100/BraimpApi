namespace Braimp.Application.Features.Courses.Queries.GetPendingCoursesList;
public class PendingCourseListResponse
{
    public List<PendingCourseLookupModel> Courses { get; set; } = 
        new List<PendingCourseLookupModel>();
}
