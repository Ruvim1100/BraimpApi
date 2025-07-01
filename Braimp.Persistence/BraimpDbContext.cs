using Braimp.Application.Abstraction;
using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Tags;
using Braimp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Infrastructure;
public class BraimpDbContext : DbContext, IBraimpDbContext, IUnitOfWork
{
    public DbSet<CourseCategory> CourseCategories => Set<CourseCategory>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseTag> CourseTags => Set<CourseTag>();
    public DbSet<CourseNews> CourseNews => Set<CourseNews>();
    public DbSet<CourseParticipant> CourseParticipants => Set<CourseParticipant>();
    public DbSet<EnrollmentRequest> EnrollmentRequests => Set<EnrollmentRequest>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonFile> Materials => Set<LessonFile>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();
    public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();
    public DbSet<AttemptAnswer> AttemptAnswers => Set<AttemptAnswer>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<AnswerText> AnswerTexts => Set<AnswerText>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<SubmissionFile> SubmissionFiles => Set<SubmissionFile>();
    public DbSet<AssignmentFile> AssignmentFiles => Set<AssignmentFile>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<LessonFile> LessonFiles => Set<LessonFile>();
    public DbSet<QuizQuestionFile> QuizQuestionFiles => Set<QuizQuestionFile>();
    public DbSet<User> Users => Set<User>();

    public BraimpDbContext(DbContextOptions<BraimpDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BraimpDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        Audit();
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    private void Audit()
    {
        var utcNow = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    break;
            }
        }
    }
}
