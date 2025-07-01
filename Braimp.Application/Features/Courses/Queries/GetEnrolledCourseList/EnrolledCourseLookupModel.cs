namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class EnrolledCourseLookupModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ThumbnailImageUrl { get; set; }
}
