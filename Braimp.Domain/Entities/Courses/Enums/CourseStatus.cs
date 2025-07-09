using System.Text.Json.Serialization;

namespace Braimp.Domain.Entities.Courses.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CourseStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Archived = 3
}
