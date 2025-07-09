using System.Text.Json.Serialization;
namespace Braimp.Domain.Entities.Courses.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GradingSystem
{
    TenPoint = 0,
    HundredPoint = 1,
    Letter = 3
}
