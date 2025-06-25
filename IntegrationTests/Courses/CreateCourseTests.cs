//using Braimp.Domain.Entities.Courses.Enums;
//using Braimp.WebApi;
//using Braimp.WebApi.Endpoints.Courses.CreateCourse;
//using Shouldly;
//using System.Net;
//using System.Net.Http.Json;

//namespace IntegrationTests.Courses;
//public class CreateCourseTests : IClassFixture<CustomWebApplicationFactory>
//{
//    private readonly HttpClient _client;

//    public CreateCourseTests(CustomWebApplicationFactory factory)
//    {
//        _client = factory.CreateClient();
//    }

//    [Fact]
//    public async Task Post_CreateCourse_ReturnsCreated()
//    {
//        // Arrange
//        var request = new Request
//        {
//            Title = "Integration Test Course",
//            Description = "Test Description",
//            GradingSystem = GradingSystem.PointsOutOf10,
//            CourseCategoryId = Guid.NewGuid()
//        };

//        // Act
//        var response = await _client.PostAsJsonAsync(ApiRoutes.Courses.Create, request);

//        // Assert
//        response.StatusCode.ShouldBe(HttpStatusCode.Created);
//    }
//}
