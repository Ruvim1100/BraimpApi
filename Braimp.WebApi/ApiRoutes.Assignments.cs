namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class Assignments
    {
        public const string Create = "api/courses/{courseId}/assignments";
        public const string Delete = "api/courses/{courseId}/assignments/{id}";
        public const string GetById = "api/courses/{courseId}/assignments/{id}";
        public const string Get = "api/courses/{courseId}/assignments";
        public const string Update = "api/courses/{courseId}/assignments/{id}";
    }
}
