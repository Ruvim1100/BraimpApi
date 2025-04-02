using Braimp.Application.Common.Exceptions;
using Braimp.Application.Courses.Commands.UpdateCourse;
using Braimp.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Tests.Courses.Commands
{
    public class UpdateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCourseCommandHandler_Succes()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(context);
            var updatedTitle = "New Title";

            // Act
            await handler.Handle(new UpdateCourseCommand
            {
                Id = BraimpContextFactory.CourseIdForUpdate,
                OwnerId = BraimpContextFactory.UserBId,
                Title = updatedTitle
            },
            CancellationToken.None
            );

            // Assert
            Assert.NotNull(await context.Courses.SingleOrDefaultAsync(course =>
                course.Id == BraimpContextFactory.CourseIdForUpdate && course.Title == updatedTitle
                ));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOrWrongId()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(context);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateCourseCommand
                {
                    Id = Guid.NewGuid(),
                    OwnerId = BraimpContextFactory.UserBId
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOrWrongUserId()
        {
            // Arrange
            var handler = new UpdateCourseCommandHandler(context);

            //Act

            //Assert

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await handler.Handle(new UpdateCourseCommand
                {
                    Id = BraimpContextFactory.CourseIdForUpdate,
                    OwnerId = BraimpContextFactory.UserAId
                },
                CancellationToken.None));
        }
    }
}
