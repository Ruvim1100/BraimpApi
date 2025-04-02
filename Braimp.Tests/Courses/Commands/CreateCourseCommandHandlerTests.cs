using Braimp.Application.Courses.Commands.CreateCourse;
using Braimp.Domain.Entities;
using Braimp.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Tests.Courses.Commands
{
    public class CreateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCourseCommandHandler_succes()
        {
            //Arrange
            var handler = new CreateCourseCommandHandler(context);
            var courseTitle = "Course Title";
            var courseDescription = "CourseDescription";

            //Act
            var courseId = await handler.Handle(
                new CreateCourseCommand
                {
                    OwnerId = BraimpContextFactory.UserAId,
                    Title = courseTitle,
                    Description = courseDescription,
                    CourseCategoryId = BraimpContextFactory.CourseCategoryId
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
                await context.Courses.SingleOrDefaultAsync(course => 
                 course.Id == courseId && course.Title == courseTitle && 
                 course.Description == courseDescription));
        }
    }
}
