using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.LearningContent;
using Braimp.Domain.Entities.Quizzes.Enums;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Users;

namespace Braimp.Infrastructure.Seed;
public static class BraimpDataSeeder
{
    public static async Task SeedAsync(this BraimpDbContext db, CancellationToken cancellationToken = default)
    {
        // 1) Users
        var owner = new User
        {
            Id = Guid.NewGuid(),
            Name = "Vasya",
            Surname = "Vasiliev",
            GivenName = "Vasya11",
            Country = "Moldova",
            
        };
        var student = new User
        {
            Id = Guid.NewGuid(),
            Name = "Hayvan",
            Surname = "Aydarli",
            GivenName = "Patkan",
            Country = "Moldova",
            
        };
        await db.Users.AddRangeAsync(owner, student);

        // 2) CourseCategories
        var catNet = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = ".NET Development"
        };
        var catJs = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = "JavaScript Frontend"
        };
        await db.CourseCategories.AddRangeAsync(catNet, catJs);

        // 3) Course
        var course1 = new Course
        {
            Id = Guid.NewGuid(),
            Title = "Introduction to ASP.NET Core",
            Description = "A basic course on building Web APIs with ASP.NET Core.",
            Status = CourseStatus.Approved,
            GradingSystem = GradingSystem.TenPoint,
            CourseCategoryId = catNet.Id
        };
        await db.Courses.AddAsync(course1, cancellationToken);

        // 4) Participants & EnrollmentRequest
        await db.CourseParticipants.AddRangeAsync(
            new CourseParticipant
            {
                Id = Guid.NewGuid(),
                CourseId = course1.Id,
                UserId = owner.Id,
                Role = CourseRole.Owner
            },
            new CourseParticipant
            {
                Id = Guid.NewGuid(),
                CourseId = course1.Id,
                UserId = student.Id,
                Role = CourseRole.Student
            }
        );
        await db.EnrollmentRequests.AddAsync(new EnrollmentRequest
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            UserId = student.Id,
            Status = EnrollmentStatus.Approved,
            
        }, cancellationToken);

        // 5) Module & Lessons
        var module1 = new Module
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            Title = "ASP.NET Core Fundamentals",
            SortIndex = 1,
        };
        await db.Modules.AddAsync(module1);

        await db.Lessons.AddRangeAsync(
            new Lesson
            {
                Id = Guid.NewGuid(),
                ModuleId = module1.Id,
                Title = "Project Configuration",
                SortIndex = 1,
                
            },
            new Lesson
            {
                Id = Guid.NewGuid(),
                ModuleId = module1.Id,
                Title = "Middleware and Services",
                SortIndex = 2,
                
            }
        );

        // 6) CourseNews
        await db.CourseNews.AddAsync(new CourseNews
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            AuthorId = owner.Id,
            Title = "Course Launched!",
            Content = "We're excited to announce that the ASP.NET Core course is now open—welcome aboard!",
        });

        // 7) Quiz
        var quiz1 = new Quiz
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            Title = "Intro Quiz",
            Description = "Test your knowledge on the fundamentals module.",
            IsPublished = true,
            
        };
        await db.Quizzes.AddAsync(quiz1);

        var q1 = new QuizQuestion
        {
            Id = Guid.NewGuid(),
            QuizId = quiz1.Id,
            Text = "What is middleware in ASP.NET Core?",
            QuestionType = QuestionType.SingleChoice,
            Weight = 1
        };
        var q2 = new QuizQuestion
        {
            Id = Guid.NewGuid(),
            QuizId = quiz1.Id,
            Text = "Which configuration formats are supported?",
            QuestionType = QuestionType.MultipleChoice,
            Weight = 1
        };
        await db.QuizQuestions.AddRangeAsync(q1, q2);

        await db.QuestionOptions.AddRangeAsync(
            new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuizQuestionId = q1.Id,
                Text = "A component that handles HTTP requests",
                IsCorrect = true
            },
            new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuizQuestionId = q1.Id,
                Text = "A JavaScript client library",
                IsCorrect = false
            },
            new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuizQuestionId = q2.Id,
                Text = "JSON",
                IsCorrect = true
            },
            new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuizQuestionId = q2.Id,
                Text = "XML",
                IsCorrect = true
            },
            new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuizQuestionId = q2.Id,
                Text = "YAML",
                IsCorrect = false
            }
        );

        // 8) Assignment & Submission
        var assignment1 = new Assignment
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            Title = "Homework #1",
            Description = "Build a minimal ASP.NET Core Web API.",
            Deadline = DateTimeOffset.UtcNow.AddDays(7),
            
        };
        await db.Assignments.AddAsync(assignment1);

        await db.Submissions.AddAsync(new Submission
        {
            Id = Guid.NewGuid(),
            AssignmentId = assignment1.Id,
            StudentId = student.Id,
            Text = "My Web API is ready, repo link: ...",
            CanEdit = false
        });

        // Сохраняем всё
        await db.SaveChangesAsync(cancellationToken);
    }
}
