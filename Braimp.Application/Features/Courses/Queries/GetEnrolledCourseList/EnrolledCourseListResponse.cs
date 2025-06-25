namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class EnrolledCourseListResponse
{
    public IList<CourseLookupModel> Courses { get; set; } = new List<CourseLookupModel>();
}
