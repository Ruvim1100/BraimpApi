using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class EnrollmentRequestLookupModel : IMapWith<EnrollmentRequest>
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public UserModel User { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EnrollmentRequest, EnrollmentRequestLookupModel>()
        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}
