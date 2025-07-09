namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class Ai
    {
        public const string Summarize= "api/ai/summarize";
        public const string Generate = "api/courses/{courseId}/quizzes/ai/generate";
    }
}
