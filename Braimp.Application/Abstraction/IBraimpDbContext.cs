using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Tags;
using Braimp.Domain.Entities.Users;
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
    DbSet<CourseImage> CourseImages { get; }
    DbSet<Module> Modules { get; }
    DbSet<Lesson> Lessons { get; }
    DbSet<LessonFile> Materials { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<Quiz> Quizzes { get; }
    DbSet<QuizQuestion> QuizQuestions { get; }
    DbSet<QuestionOption> QuestionOptions { get; }
    DbSet<QuizAttempt> QuizAttempts { get; }
    //DbSet<QuestionAnswer> QuestionAnswers { get; }
    //DbSet<AnswerOption> AnswerOptions { get; }
    DbSet<Assignment> Assignments { get; }
    DbSet<Submission> Submissions { get; }
    DbSet<SubmissionFile> SubmissionFiles { get; }
    DbSet<AssignmentFile> AssignmentFiles { get; }
    DbSet<Resource> Resources { get; }
    DbSet<LessonFile> LessonFiles { get; }
    DbSet<QuizQuestionFile> QuizQuestionFiles{ get; }
    DbSet<User> Users { get; }
}
