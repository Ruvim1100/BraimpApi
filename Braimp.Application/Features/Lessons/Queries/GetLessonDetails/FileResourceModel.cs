namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class FileResourceModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;

}
