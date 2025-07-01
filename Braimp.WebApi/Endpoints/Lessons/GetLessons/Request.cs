using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Lessons.GetLessons;
public class Request
{
    [FromRoute]
    [Required]
    public Guid CourseId { get; set; }

    [FromRoute]
    [Required]
    public Guid ModuleId { get; set; }

}
