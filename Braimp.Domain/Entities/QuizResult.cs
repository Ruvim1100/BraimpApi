using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class QuizResult : BaseEntity
    {
        public Guid StudentId { get; set; }
        public decimal Score { get; set; }
        public decimal? Grade { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int IncorrectAnswerCount { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public int AttemptNumber { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
