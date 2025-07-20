using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Users;

namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class UserModel : IMapWith<User>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserModel>();
    }
}
