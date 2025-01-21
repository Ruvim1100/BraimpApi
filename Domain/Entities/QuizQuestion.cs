using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class QuizQuestion : BaseEntity
    {
        public string Text { get; set; }
        public QuestionType QuestionType { get; set; }
        public string? MediaUrl { get; set; }
        public int Weight { get; set; } = 1;
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<QuizOption> QuizOptions { get; set; }
            = new List<QuizOption>();
    }
}
