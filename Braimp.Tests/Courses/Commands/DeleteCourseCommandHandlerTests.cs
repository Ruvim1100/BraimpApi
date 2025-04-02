using Braimp.Application.Common.Exceptions;
using Braimp.Application.Courses.Commands.CreateCourse;
using Braimp.Application.Courses.Commands.DeleteCourse;
using Braimp.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Tests.Courses.Commands
{
    public class DeleteCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteCourseCommandHandler_succes()
        {
            // Arrange
            var handler = new DeleteCourseCommandHandler(context);

            // Act
            await handler.Handle(new DeleteCourseCommand
            {
                Id = BraimpContextFactory.CourseIdForDelete,
                OwnerId = BraimpContextFactory.UserAId
            }
            , CancellationToken.None);

            //Assert
            Assert.Null(await context.Courses.SingleOrDefaultAsync(course => 
                course.Id == BraimpContextFactory.CourseIdForDelete));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOrWrongId()
        {
            // Arrange
            var handler = new DeleteCourseCommandHandler(context);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCourseCommand
                    {
                        Id = Guid.NewGuid(),
                        OwnerId = BraimpContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOrWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteCourseCommandHandler(context);
            var createHandler = new CreateCourseCommandHandler(context);
            var courseId = await createHandler.Handle(
                new CreateCourseCommand
                {
                    OwnerId = BraimpContextFactory.UserAId,
                    Title = "CourseTitle",
                    Description = "CourseDescription4",
                    CourseCategoryId = BraimpContextFactory.CourseCategoryId
                },
                CancellationToken.None);

            // Act

            //Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            await deleteHandler.Handle(
                new DeleteCourseCommand
                {
                    Id = courseId,
                    OwnerId = BraimpContextFactory.UserBId,
                },
                CancellationToken.None));
        }
    }
}
