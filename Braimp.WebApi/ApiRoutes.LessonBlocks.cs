namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class LessonBlocks
    {
        public const string Create = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/blocks";
        public const string Get = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/blocks";
        public const string Update = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/blocks/{id}";
        public const string Delete = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/blocks/{id}";
    }
}
