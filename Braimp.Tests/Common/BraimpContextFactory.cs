using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using Braimp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Tests.Common
{
    public class BraimpContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid CourseCategoryId = Guid.NewGuid();

        public static Guid CourseIdForDelete = Guid.NewGuid();
        public static Guid CourseIdForUpdate = Guid.NewGuid();

        public static BraimpDbContext Create()
        {
            var options = new DbContextOptionsBuilder<BraimpDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BraimpDbContext(options);
            context.Database.EnsureCreated();

            context.CourseCategories.Add(new CourseCategory
            {
                Id = CourseCategoryId,
                Name = "Test Category"
            });

            context.Courses.AddRange(
                new Course
                {
                    Id = Guid.Parse("{EA8C646B-26CB-4258-848E-2FDED0D8B5AC}"),
                    OwnerId = UserAId,
                    Title = "Course1",
                    Description = "Description2",
                    Status = CourseStatus.Approved,
                    CreatedAt = DateTime.UtcNow,
                    CourseCategoryId = CourseCategoryId
                },
                new Course
                {
                    Id = Guid.Parse("{A00298E4-A885-414A-AAA1-460AB7FD8B02}"),
                    OwnerId = UserBId,
                    Title = "Course2",
                    Description = "Description2",
                    Status = CourseStatus.Approved,
                    CreatedAt = DateTime.UtcNow,
                    CourseCategoryId = CourseCategoryId
                },
                new Course
                {
                    Id = CourseIdForDelete,
                    OwnerId = UserAId,
                    Title = "Course3",
                    Description = "Description3",
                    Status = CourseStatus.Approved,
                    CreatedAt = DateTime.UtcNow,
                    CourseCategoryId = CourseCategoryId
                },
                new Course
                {
                    Id = CourseIdForUpdate,
                    OwnerId = UserBId,
                    Title = "Course4",
                    Description = "Description4",
                    Status = CourseStatus.Approved,
                    CreatedAt = DateTime.UtcNow,
                    CourseCategoryId = CourseCategoryId
                });

            context.SaveChanges();
            return context;
        }

        public static void Destroy(BraimpDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
