using Braimp.Application.Exceptions;
using Braimp.Application.Features.Courses.Commands.DeleteCourse;
using Braimp.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Braimp.Tests.Courses.Commands;
public class DeleteCourseCommandHandlerTests : TestCommandBase
{
    private readonly Mock<ILogger<DeleteCourseCommandHandler>> _mockLogger;

    public DeleteCourseCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<DeleteCourseCommandHandler>>();
    }

    [Fact]
    public async Task DeleteCommandHandler_Succes()
    {
        // Arrange 
        var handler = new DeleteCourseCommandHandler(context, context, _mockLogger.Object);

        var command = new DeleteCourseCommand
        {
            Id = BraimpContextFactory.CourseIdForDelete
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var course = await context.Courses.FindAsync(command.Id);
        course.ShouldBeNull();
    }

    [Fact]
    public async Task DeleteCommandHandler_ShouldThrow_WhenCourseDoesNotExist()
    {
        // Arrange
        var handler = new DeleteCourseCommandHandler(context, context, _mockLogger.Object);
        var randomId = Guid.NewGuid();

        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => handler.Handle(
            new DeleteCourseCommand { Id = randomId }, CancellationToken.None));
    }
}
