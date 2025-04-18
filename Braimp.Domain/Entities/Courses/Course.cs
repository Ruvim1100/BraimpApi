using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Tags;

namespace Braimp.Domain.Entities.Courses;
public class Course : BaseEntity<Guid>, IAuditable
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CourseStatus Status { get; set; } = CourseStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public GradingSystem GradingSystem { get; set; } = GradingSystem.PointsOutOf10;
    public string? CoverImageUrl { get; set; }
    public string? BackgroundColor { get; set; }
    public string? LogoUrl { get; set; }
    public Guid CourseCategoryId { get; set; }
    public CourseCategory CourseCategory { get; set; } = null!;
    public ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
        = new List<EnrollmentRequest>();
    public ICollection<CourseParticipant> Participants { get; set; }
        = new List<CourseParticipant>();
    public ICollection<CourseTag> Tags { get; set; }
        = new List<CourseTag>();
    public ICollection<Module> Modules { get; set; }
        = new List<Module>();
    public ICollection<CourseNews> News { get; set; }
        = new List<CourseNews>();
    public ICollection<Quiz> Quizzes { get; set; }
        = new List<Quiz>();
    public ICollection<Assignment> Assignments { get; set; }
        = new List<Assignment>();
    public ICollection<Notification> Notifications { get; set; }
        = new List<Notification>();
}
