namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class Courses
    {
        public const string Create = "api/courses";
        public const string Delete = "api/courses/{id}";
        public const string GetById = "api/courses/{id}";
        public const string Get = "api/courses";
        public const string Update = "api/courses/{id}";
        public const string GetEnrolled = "api/courses/enrolled";
    }
}
