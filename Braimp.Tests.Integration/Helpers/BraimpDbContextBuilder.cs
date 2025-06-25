using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Tests.Integration.Helpers;
public class BraimpDbContextBuilder : IDisposable
{
    private readonly BraimpDbContext _context;

    public BraimpDbContextBuilder(string? dbName = null)
    {
        dbName ??= $"TestDb_{Guid.NewGuid()}";

        var options = new DbContextOptionsBuilder<BraimpDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        _context = new BraimpDbContext(options);
    }

    public BraimpDbContext GetContext()
    {
        _context.Database.EnsureCreated();
        return _context;
    }

    public void SeedCourses(int count = 1)
    {
        var category = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        _context.CourseCategories.Add(category);

        for (int i = 0; i < count; i++)
        {
            _context.Courses.Add(new Course
            {
                Id = Guid.NewGuid(),
                Title = $"Test Course {i + 1}",
                Description = $"Description {i + 1}",
                CourseCategory = category,
                Status = CourseStatus.Approved
            });
        }

        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
