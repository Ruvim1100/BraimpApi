using Microsoft.AspNetCore.Mvc;

namespace Braimp.WebApi.Endpoints.Lessons.GetLessons;
public class Request
{
    [FromRoute]
    public Guid CourseId { get; set; }

    [FromRoute]
    public Guid ModuleId { get; set; }

    [FromQuery]
    public string? SearchTerm { get; set; }

    [FromQuery]
    public bool? IsPublished { get; set; }
}
