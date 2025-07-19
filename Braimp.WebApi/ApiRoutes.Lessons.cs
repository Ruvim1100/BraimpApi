namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class Lessons
    {
        public const string Create = "api/courses/{courseId}/modules/{moduleId}/lessons";
        public const string Delete = "api/courses/{courseId}/modules/{moduleId}/lessons/{id}";
        public const string GetById = "api/courses/{courseId}/modules/{moduleId}/lessons/{id}";
        public const string Get = "api/courses/{courseId}/modules/{moduleId}/lessons";
        public const string GetPublished = "api/courses/{courseId}/modules/{moduleId}/lessons/published";
        public const string Update = "api/courses/{courseId}/modules/{moduleId}/lessons/{id}";
        public const string Translate = "api/course/{courseId}/moduleId/{moduleId}/lessons{lessonId}";
    }
}
