namespace Braimp.WebApi;
public partial class ApiRoutes
{
    public class LessonFiles
    {
        public const string Create = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/lessonFiles";
        public const string Delete = "api/courses/{courseId}/modules/{moduleId}/lessons/{lessonId}/lessonFiles/{id}";
    }
}
