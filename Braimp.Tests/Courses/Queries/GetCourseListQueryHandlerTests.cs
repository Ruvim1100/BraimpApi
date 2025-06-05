using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetCourseListQueryHandler>> _mockLogger;

    public GetCourseListQueryHandlerTests(QueryTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
        _mockLogger = new Mock<ILogger<GetCourseListQueryHandler>>();
    }
    [Fact]
    public async Task GetCourseListQueryHandler_Succes()
    {
        // Arrange
        var handler = new GetCourseListQueryHandler(_dbContext, _mapper, _mockLogger.Object);
        var query = new GetCourseListQuery
        {
            Page = 1,
            PageSize = 10
        };

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.TotalCount.ShouldBe(4);
        result.ShouldBeOfType<PaginationResult<CourseLookupModel>>();

    }
}
