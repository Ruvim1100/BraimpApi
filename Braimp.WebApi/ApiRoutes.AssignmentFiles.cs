namespace Braimp.WebApi;
internal partial class ApiRoutes
{
    internal class AssignmentFiles
    {
        public const string Create = "api/courses/{courseId}/assignments/{assignmentId}/assignmentFiles";
        public const string Delete = "api/courses/{courseId}/assignments/{assignmentId}/assignmentFiles/{id}";
        public const string GetById = "api/courses/{courseId}/assignments/{assignmentId}/assignmentFiles/{id}";
        public const string Get = "api/courses/{courseId}/assignments/{assignmentId}/assignmentFiles";
        public const string Update = "api/courses/{courseId}/assignments/{assignmentId}/assignmentFiles/{id}";
    }
}
