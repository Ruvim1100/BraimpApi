using Braimp.Application.Abstraction;
using Braimp.Application.Features.Courses.Queries.GetCourseList;
using Braimp.Application.Pagination;
using Braimp.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Braimp.Tests.Courses.Queries;

[Collection("QueryCollection")]
public class GetCourseListQueryHandlerTests
{
    private readonly IBraimpDbContext _dbContext;
    private readonly Mock<ILogger<GetCourseListQueryHandler>> _mockLogger;
    private readonly Mock<IBlobStorageService> _mockBlobStorageService;

    public GetCourseListQueryHandlerTests(QueryTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mockLogger = new Mock<ILogger<GetCourseListQueryHandler>>();
        _mockBlobStorageService = new Mock<IBlobStorageService>();
    }

    [Fact]
    public async Task GetCourseListQueryHandler_Success()
    {
        // Arrange
        var handler = new GetCourseListQueryHandler(
            _dbContext,
            _mockLogger.Object,
            _mockBlobStorageService.Object);

        var query = new GetCourseListQuery
        {
            Page = 1,
            PageSize = 10
        };

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.TotalCount.ShouldBe(4);
        result.ShouldBeOfType<PaginationResult<CourseLookupModel>>();
    }
}
