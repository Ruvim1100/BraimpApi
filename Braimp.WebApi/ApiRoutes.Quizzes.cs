namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class Quizzes
    {
        public const string Create = "api/courses/{courseId}/quizzes";
        public const string Delete = "api/courses/{courseId}/quizzes/{id}";
        public const string GetById = "api/courses/{courseId}/quizzes/{id}";
        public const string Get = "api/courses/{courseId}/quizzes";
        public const string GetPublished = "api/courses/{courseId}/quizzes/published";
        public const string Update = "api/courses/{courseId}/quizzes/{id}";
        public const string Generate = "api/courses/{courseId}/quizzes/ai/generate";
    }
}
