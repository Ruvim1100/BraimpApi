using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities;
public class Resource: BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
