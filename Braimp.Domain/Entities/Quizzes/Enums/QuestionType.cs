using System.Text.Json.Serialization;

namespace Braimp.Domain.Entities.Quizzes.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QuestionType
{
    SingleChoice = 0,
    MultipleChoice = 1,
    Text = 2
}
