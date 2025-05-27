namespace Braimp.WebApi;
internal partial class ApiRoutes
{
    internal class SubmissionFiles
    {
        public const string Create = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{submissionId}/submissionFiles";
        public const string Delete = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{submissionId}/submissionFiles/{id}";
        public const string GetById = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{submissionId}/submissionFiles/{id}";
        public const string Get = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{submissionId}/submissionFiles";
        public const string Update = "api/courses/{courseId}/assignments/{assignmentId}/submissions/{submissionId}/submissionFiles/{id}"; 
    }
}
