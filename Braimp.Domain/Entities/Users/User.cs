using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.Users;
public class User : BaseEntity<Guid>, IAuditable
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;       
    public string Surname { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty; 
    public string Country { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }         

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<CourseParticipant> Courses { get; set; } 
        = new List<CourseParticipant>();
}
