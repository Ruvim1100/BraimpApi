namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class EnrollmentRequestLookupModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ThumbnailImageUrl { get; set; }
}
