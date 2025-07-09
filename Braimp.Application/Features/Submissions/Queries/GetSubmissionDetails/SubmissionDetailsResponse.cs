using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Assignments;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
public class SubmissionDetailsResponse : IMapWith<Submission>
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid? ReviewerId { get; set; }
    public string? Text { get; set; }
    public decimal? Grade { get; set; }
    public string? ReviewComment { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReviewedAt { get; set; }
    public string DownloadFileUrl { get; set; } = string.Empty;
    public StudentModel Student { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Submission, SubmissionDetailsResponse>()
           .ForMember(dest => dest.Student, opt => opt.Ignore())
           .ForMember(dest => dest.DownloadFileUrl, opt => opt.Ignore());
    }

}
