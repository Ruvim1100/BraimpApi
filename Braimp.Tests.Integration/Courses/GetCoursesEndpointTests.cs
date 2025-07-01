using Braimp.Application.Features.Courses.Queries.GetCourseList;
using Braimp.Application.Pagination;
using Braimp.Tests.Integration.Helpers;
using Braimp.WebApi.Endpoints.Courses.GetCourses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Braimp.Tests.Integration.Courses;
public class GetCoursesEndpointTests
{
    [Fact]
    public async Task Handler_WhenCalled_ReturnsOkWithAllSeededCourses()
    {
        // Arrange
        const int numberOfCourses = 3;
        using var builder = new BraimpDbContextBuilder();
        builder.SeedCourses(numberOfCourses);
        var dbContext = builder.GetContext();

        var mapper = TestHelpers.CreateMapper();
        var mediator = TestHelpers.CreateMediator(dbContext, mapper);

        var endpoint = new Endpoint();

        var query = new GetCourseListQuery
        {
            Page = 1,
            PageSize = 10,
            SortBy = "CreatedAt",
            Descending = true,
        };

        // Act
        var result = await endpoint.Handler(query, mediator, CancellationToken.None);

        // Assert
        var ok = Assert.IsType<Ok<PaginationResult<CourseLookupModel>>>(result);
        var payload = ok.Value;

        Assert.NotNull(payload);
        Assert.Equal(numberOfCourses, payload.Items.Count);
        Assert.Equal(1, payload.Page);
        Assert.Equal(10, payload.PageSize);
        Assert.Equal(numberOfCourses, payload.TotalCount);
    }
}
