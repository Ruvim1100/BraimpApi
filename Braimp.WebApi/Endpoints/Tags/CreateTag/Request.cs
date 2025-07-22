using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Tags.CreateTag;
public class Request
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}
