using Braimp.Application.Abstraction;
using Braimp.Application.Features.Courses.Commands.CreateCourse;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Braimp.Tests.Courses.Commands;
public class CreateCourseCommandHandlerTests : TestCommandBase
{
    private readonly Mock<ICurrentUserService> _mockCurrentUser;
    private readonly Mock<ILogger<CreateCourseCommandHandler>> _mockLogger;

    public CreateCourseCommandHandlerTests()
    {
        _mockCurrentUser = new Mock<ICurrentUserService>();
        _mockLogger = new Mock<ILogger<CreateCourseCommandHandler>>();
    }


    [Fact]
    public async Task CreateCourseCommandHandler_CreatesCourse_WithDescription()
    {
        // Arrange
        var userId = BraimpContextFactory.UserAId;
        _mockCurrentUser.Setup(x => x.UserId).Returns(userId);

        var handler = new CreateCourseCommandHandler(context, context, _mockCurrentUser.Object, _mockLogger.Object);
        var command = new CreateCourseCommand
        {
            Title = "New Test Course",
            Description = "Test Description",
            CourseCategoryId = BraimpContextFactory.CourseCategoryId
        };

        // Act
        var courseId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var course = await context.Courses.FindAsync(courseId);
        course.ShouldNotBeNull();
        course.Title.ShouldBe("New Test Course");
        course.Description.ShouldBe("Test Description");
        course.GradingSystem.ShouldBe(GradingSystem.HundredPoint);
        course.CourseCategoryId.ShouldBe(BraimpContextFactory.CourseCategoryId);

        var participant = course.Participants.Single(p => p.UserId == userId);
        participant.Role.ShouldBe(CourseRole.Owner);
    }
}
