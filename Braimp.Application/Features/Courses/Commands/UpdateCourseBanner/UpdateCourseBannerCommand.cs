using MediatR;
using System.Text;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourseBanner;
public class UpdateCourseBannerCommand : IRequest
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
