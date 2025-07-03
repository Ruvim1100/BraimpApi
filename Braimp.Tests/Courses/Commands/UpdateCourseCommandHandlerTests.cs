using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.Tests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Braimp.Tests.Courses.Commands;
public class UpdateCourseCommandHandlerTests : TestCommandBase
{
    private readonly Mock<ILogger<UpdateCourseCommandHandler>> _mocklogger;
    public UpdateCourseCommandHandlerTests()
    {
        _mocklogger = new Mock<ILogger<UpdateCourseCommandHandler>>();
    }

    [Fact]
    public async Task UpdateCourseCommandHandler_Succes()
    {
        // Arrange
        var newTitle = "I Don't remember";
        var handler = new UpdateCourseCommandHandler(context, context, _mocklogger.Object);
        var command = new UpdateCourseCommand
        {
            Id = BraimpContextFactory.CourseIdForUpdate,
            Title = newTitle,
        };

        // Act
        var resultId = await handler.Handle(command, CancellationToken.None);

        // Assert
        var course = await context.Courses.FindAsync(command.Id);
        course.ShouldNotBeNull();
        course!.Title.ShouldBe(newTitle);
    }

    [Fact]
    public async Task UpdateCourseCommandHandler_ShouldThrow_WhenCourseNotFound()
    {
        // Arrange
        var handler = new UpdateCourseCommandHandler(context, context, _mocklogger.Object);
        var randomId = Guid.NewGuid();
        var command = new UpdateCourseCommand
        {
            Id = randomId,
            Title = "Any",
            Description = "Any"
        };
        // Act & Assert
        await Should.ThrowAsync<InvalidOperationException>(() => 
        handler.Handle(command, CancellationToken.None));
    }
}
