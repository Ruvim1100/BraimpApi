namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class QuizAttempts
    {
        public const string Create = "api/courses/{courseId}/quizzes/{quizId}/attempts";
        public const string Delete = "api/courses/{courseId}/quizzes/{quizId}/attempts/{attemptId}";
        public const string GetById = "api/courses/{courseId}/quizzes/{quizId}/attempts/{attemptId}";
        public const string Get = "api/courses/{courseId}/quizzes/{quizId}/attempts";
        public const string Update = "api/courses/{courseId}/quizzes/{quizId}/attempts/{attemptId}";
        public const string Submit = "api/courses/{courseId}/quizzes/{quizId}/attempts/{attemptId}/submit";
    }
}
