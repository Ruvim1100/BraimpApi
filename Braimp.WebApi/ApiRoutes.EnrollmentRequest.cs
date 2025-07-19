namespace Braimp.WebApi;
public partial class ApiRoutes
{
    internal class EnrollmentRequest
    {
        public const string Create = "api/courses/{courseId}/enrollmentRequests";
        public const string Delete = "api/courses/{courseId}/enrollmentRequests/{id}";
        public const string GetById = "api/courses/{courseId}/enrollmentRequests/{id}";
        public const string Get = "api/courses/{courseId}/enrollmentRequests";
        public const string Update = "api/courses/{courseId}/enrollmentRequests/{id}";
    }
}
