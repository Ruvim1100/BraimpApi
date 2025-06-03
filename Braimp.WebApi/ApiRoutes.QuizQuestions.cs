namespace Braimp.WebApi;
internal partial class ApiRoutes
{
    internal class QuizQuestions
    {
        public const string Create = "api/courses/{courseId}/quizzes{quizId}/quizQuestions";
        public const string Delete = "api/courses/{courseId}/quizzes{quizId}/quizQuestions/{id}";
        public const string GetById = "api/courses/{courseId}/quizzes{quizId}/quizQuestions/{id}";
        public const string Get = "api/courses/{courseId}/quizzes{quizId}/quizQuestions";
        public const string Update = "api/courses/{courseId}/quizzes{quizId}/quizQuestions/{id}";
    }
}
