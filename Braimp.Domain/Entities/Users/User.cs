using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Users;
public class User : BaseEntity<Guid>, IAuditable
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string GivenName {  get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
