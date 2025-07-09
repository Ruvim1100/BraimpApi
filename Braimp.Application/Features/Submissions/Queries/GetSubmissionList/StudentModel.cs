using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Users;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class StudentModel : IMapWith<User>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, StudentModel>();
    }
}
