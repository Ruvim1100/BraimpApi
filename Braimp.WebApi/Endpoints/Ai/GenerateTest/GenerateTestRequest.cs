using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Ai.GenerateTest;
public record GenerateTestRequest([Required, MinLength(100)] string content);
