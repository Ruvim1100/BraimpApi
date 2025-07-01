namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class Modules
    {
        public const string Create = "api/courses/{courseId}/modules";
        public const string Delete = "api/courses/{courseId}/modules/{id}";
        public const string GetById = "api/courses/{courseId}/modules/{id}";
        public const string Get = "api/courses/{courseId}/modules";
        public const string GetPublished = "api/courses/{courseId}/modules/published";
        public const string Update = "api/courses/{courseId}/modules/{id}";
    }
}
