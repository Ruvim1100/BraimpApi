using AutoMapper;
using Braimp.Application.Courses.Queries.GetCourseDetails;
using Braimp.Persistence;
using Braimp.Tests.Common;
using Shouldly;

namespace Braimp.Tests.Courses.Queries
{
    [Collection("QueryCollection")]
    public class GetCourseDetailsQueryHandlerTests
    {
        private readonly BraimpDbContext Context;
        private readonly IMapper Mapper;

        public GetCourseDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCourseDetailsQueryHandler_Succes()
        {
            // Arrange
            var handler = new GetCourseDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseDetailQuery
                {
                    Id = Guid.Parse("{A00298E4-A885-414A-AAA1-460AB7FD8B02}"),
                    OwnerId = BraimpContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseDetailsVm>();
            result.Title.ShouldBe("Course2");
        }
    }
}
