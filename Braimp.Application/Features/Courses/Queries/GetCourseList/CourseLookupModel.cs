namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseLookupModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? ThumbnailImageUrl { get; set; }
}
