namespace Braimp.WebApi.Endpoints.Ai.SummarizeLesson;
using System.ComponentModel.DataAnnotations;

public record SummarizeLessonRequest([Required, MinLength(100)] string content);