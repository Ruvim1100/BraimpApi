using System.Text;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;
public class ResourceModel
{
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
