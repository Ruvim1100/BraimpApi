using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Assignments;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class SubmissionLookupModel : IMapWith<Submission>
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Submission, SubmissionLookupModel>();
    }
}
