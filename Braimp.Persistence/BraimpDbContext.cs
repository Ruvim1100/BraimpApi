using Braimp.Application;
using Braimp.Domain.Entities;
using Braimp.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Persistence
{
    sealed class BraimpDbContext : DbContext, IBraimpDbContext
    {
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<CourseNews> CourseNews { get; set; }
        public DbSet<CourseSettings> CourseSettings { get; set; }
        public DbSet<CourseParticipant> CourseParticipants { get; set; }
        public DbSet<EnrollmentRequest> EnrollmentRequests { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionAttachment> SubmissionAttachments { get; set; }

        public BraimpDbContext(DbContextOptions<BraimpDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new CourseTagConfiguration());
            modelBuilder.ApplyConfiguration(new CourseNewsConfiguration());
            modelBuilder.ApplyConfiguration(new CourseSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new CourseParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new QuizConfiguration());
            modelBuilder.ApplyConfiguration(new QuizQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new QuizOptionConfiguration());
            modelBuilder.ApplyConfiguration(new QuizResultConfiguration());
            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionConfiguration());
            modelBuilder.ApplyConfiguration(new SubmissionAttachmentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
