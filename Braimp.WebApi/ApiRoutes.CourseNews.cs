namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class CourseNews
    {
        public const string Create = "api/courses/{courseId}/news";
        public const string Delete = "api/courses/{courseId}/news/{id}";
        public const string GetById = "api/courses/{courseId}/news/{id}";
        public const string Get = "api/courses/{courseId}/news";
        public const string Update = "api/courses/{courseId}/news/{id}";
    }
}
