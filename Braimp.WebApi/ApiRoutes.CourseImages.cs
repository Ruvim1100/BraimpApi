namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class CourseImages
    {
        public const string Create = "api/courses/{courseId}/courseImages";
        public const string Delete = "api/courses/{courseId}/courseImages/{id}";
        public const string GetById = "api/courses/{courseId}/courseImages/{id}";
        public const string Get = "api/courses/{courseId}/courseImages";
        public const string Update = "api/courses/{courseId}/courseImages/{id}";
    }
}
