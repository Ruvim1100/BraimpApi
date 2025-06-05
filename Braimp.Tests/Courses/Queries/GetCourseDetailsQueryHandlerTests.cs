using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Application.Features.Courses.Queries.GetCourseDetails;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Braimp.Tests.Courses.Queries;
[Collection("QueryCollection")]
public class GetCourseDetailsQueryHandlerTests
{
    private readonly IBraimpDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetCourseDetailsQueryHandler>> _mockLogger;
    private readonly Mock<ICurrentUserService> _mockCurrentUser;

    public GetCourseDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
        _mockLogger = new Mock<ILogger<GetCourseDetailsQueryHandler>>();
        _mockCurrentUser = new Mock<ICurrentUserService>();
    }

    [Fact]
    public async Task GetCourseDetailsQueryHandler_Succes()
    {
        // Arrange
        var handler = new GetCourseDetailsQueryHandler(_dbContext, _mapper, _mockLogger.Object, _mockCurrentUser.Object);
        var query = new GetCourseDetailQuery
        {
            Id = Guid.Parse("{EA8C646B-26CB-4258-848E-2FDED0D8B5AC}")
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CourseDetailsResponse>();
        result.OwnerId.ShouldBe(BraimpContextFactory.UserAId);
        result.Title.ShouldBe("Course1");
        result.Description.ShouldBe("Description2");
    }

    [Fact]
    public async Task GetCourseDetailsQueryHandler_ShouldThrow_WhenCourseNotFound()
    {
        // Arrange
        var handler = new GetCourseDetailsQueryHandler(_dbContext, _mapper, _mockLogger.Object, _mockCurrentUser.Object);
        var query = new GetCourseDetailQuery 
        { 
            Id = Guid.NewGuid() 
        };

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => 
            handler.Handle(query, CancellationToken.None));
    }
}