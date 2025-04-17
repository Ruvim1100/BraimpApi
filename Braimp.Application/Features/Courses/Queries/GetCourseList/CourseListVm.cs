namespace Braimp.Application.Features.Courses.Queries.GetCourseList
{
    public class CourseListVm 
    {
        public IList<CourseLookupDto> Courses { get; set; } = new List<CourseLookupDto>();
    }
}
