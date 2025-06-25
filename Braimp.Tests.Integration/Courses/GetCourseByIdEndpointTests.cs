using Braimp.Application.Features.Courses.Queries.GetCourseDetails;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.Courses;
using Braimp.Tests.Integration.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Braimp.WebApi.Endpoints.Courses.GetCourseById;

namespace Braimp.Tests.Integration.Courses;
public class GetCourseByIdEndpointTests
{
    [Fact]
    public async Task ReturnsCourseDetails()
    {
        // Arrange
        using var builder = new BraimpDbContextBuilder();
        var dbContext = builder.GetContext();

        var category = new CourseCategory
        {
            Id = Guid.NewGuid(),
            Name = "Test Category"
        };

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = "Test Course",
            Description = "Test Description",
            Status = CourseStatus.Approved,
            CreatedAt = DateTimeOffset.UtcNow,
            CourseCategory = category
        };

        dbContext.CourseCategories.Add(category);
        dbContext.Courses.Add(course);
        dbContext.SaveChanges();

        var mapper = TestHelpers.CreateMapper();
        var mediator = TestHelpers.CreateMediator(dbContext, mapper);

        var endpoint = new Endpoint();

        // Act
        var result = await endpoint.Handler(course.Id, mediator, CancellationToken.None);

        // Assert
        var ok = Assert.IsType<Ok<CourseDetailsResponse>>(result);
        var value = ok.Value;

        Assert.NotNull(value);
        Assert.Equal(course.Id, value.Id);
        Assert.Equal(course.Title, value.Title);
        Assert.Equal(course.Description, value.Description);
        Assert.Equal(course.Status.ToString(), value.Status);
        Assert.Equal(category.Name, value.CourseCategory);
    }
}
