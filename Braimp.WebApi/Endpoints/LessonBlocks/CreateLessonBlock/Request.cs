using Braimp.Domain.Entities.LearningContent.Enums;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.LessonBlocks.CreateLessonBlock;
public class Request
{
    [Required]
    [MaxLength(50)]
    public LessonBlockType Type { get; set; }
    [Required]
    [MaxLength(10000)]
    public string Content { get; set; } = string.Empty;
}
