using AutoMapper;
using Braimp.Application.Courses.Queries.GetCourseList;
using Braimp.Persistence;
using Braimp.Tests.Common;
using Shouldly;

namespace Braimp.Tests.Courses.Queries
{
    [Collection("QueryCollection")]
    public class GetCourseListQueryHandlerTests
    {
        private readonly BraimpDbContext Context;
        private readonly IMapper Mapper;

        public GetCourseListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }
        [Fact]
        public async Task GetCourseListQueryHandler_Succes()
        {
            // Arrange
            var handler = new GetCourseListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseListQuery
                {

                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseListVm>();
            result.Courses.Count.ShouldBe(4);
        }
    }
}
