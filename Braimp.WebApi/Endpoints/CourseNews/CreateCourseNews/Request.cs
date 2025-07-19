using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.CourseNews.CreateCourseNews;
public class Request
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title must be at most 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required")]
    [MaxLength(1000, ErrorMessage = "Content must be at most 1000 characters")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "File display name is required")]
    public string FileDisplayName { get; set; } = string.Empty;

    [Required(ErrorMessage = "File is required")]
    public IFormFile File { get; set; } = null!;
}
