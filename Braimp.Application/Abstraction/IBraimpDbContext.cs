using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Tags;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Abstraction
{
    public interface IBraimpDbContext
    {
        DbSet<CourseCategory> CourseCategories { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<CourseTag> CourseTags { get; set; }
        DbSet<CourseNews> CourseNews { get; set; }
        DbSet<CourseParticipant> CourseParticipants { get; set; }
        DbSet<EnrollmentRequest> EnrollmentRequests { get; set; }
        DbSet<Module> Modules { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Quiz> Quizzes { get; set; }
        DbSet<QuizQuestion> QuizQuestions { get; set; }
        DbSet<QuizOption> QuizOptions { get; set; }
        DbSet<QuizResult> QuizResults { get; set; }
        DbSet<Assignment> Assignments { get; set; }
        DbSet<Submission> Submissions { get; set; }
        DbSet<SubmissionAttachment> SubmissionAttachments { get; set; }
    }
}
