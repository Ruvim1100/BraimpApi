using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Users;

namespace Braimp.Application.Features.Courses.Queries.GetStudentList;
public class StudentLookupModel : IMapWith<User>
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;    
    public string? ProfileImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, StudentLookupModel>();
    }
}
