namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class Submissions
    {
        public const string Create = "api/courses/{courseId}/assignments/{assignmentId}/submissions";
        public const string Delete = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{id}";
        public const string GetById = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{id}";
        public const string Get = "api/courses/{courseId}/assignments/{assignmentId}/submissions";
        public const string Update = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{id}";
        public const string Grade = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{id}/grade";
    }
}
