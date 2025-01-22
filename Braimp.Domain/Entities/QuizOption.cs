using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class QuizOption : BaseEntity
    {
        public string Text { get; set; }
        public string? MediaUrl { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuizQuestionId { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
    }
}