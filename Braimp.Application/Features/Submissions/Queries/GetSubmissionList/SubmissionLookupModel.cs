using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Assignments;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class SubmissionLookupModel : IMapWith<Submission>
{
    public Guid Id { get; set; }
    public decimal? Grade { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? Text { get; set; }
    public Guid StudentId { get; set; }
    public StudentModel Student { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Submission, SubmissionLookupModel>()
            .ForMember(dest => dest.Student, opt => opt.Ignore());
    }
}
