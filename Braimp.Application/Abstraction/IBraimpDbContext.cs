using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Tags;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Abstraction;
public interface IBraimpDbContext
{
    DbSet<CourseCategory> CourseCategories { get; }
    DbSet<Tag> Tags { get; }
    DbSet<Course> Courses { get; }
    DbSet<CourseTag> CourseTags { get; }
    DbSet<CourseNews> CourseNews { get; }
    DbSet<CourseParticipant> CourseParticipants { get; }
    DbSet<EnrollmentRequest> EnrollmentRequests { get; }
    DbSet<Module> Modules { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<LessonFile> Materials { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<Quiz> Quizzes { get; }
    DbSet<QuizQuestion> QuizQuestions { get; }
    DbSet<QuizOption> QuizOptions { get; }
    DbSet<QuizResult> QuizResults { get; }
    DbSet<Assignment> Assignments { get; }
    DbSet<Submission> Submissions { get; }
    DbSet<SubmissionFile> SubmissionAttachments { get; }
    DbSet<AssignmentFile> AssignmentAttachments { get; }
}
