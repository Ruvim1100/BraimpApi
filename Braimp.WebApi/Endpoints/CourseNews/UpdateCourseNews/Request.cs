using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.CourseNews.UpdateCourseNews;
public class Request
{
    [MaxLength(100, ErrorMessage = "Title must be at most 100 characters")]
    public string? Title { get; set; }

    [MaxLength(1000, ErrorMessage = "Content must be at most 1000 characters")]
    public string? Content { get; set; }

    public string? FileDisplayName { get; set; }

    public IFormFile? File { get; set; }
}
