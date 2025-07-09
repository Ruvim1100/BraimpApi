using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.LessonBlocks.UpdateLessonBlock;
public class Request
{
    [FromBody]
    [MaxLength(10000)]
    public string Content { get; set; } = string.Empty;
}
