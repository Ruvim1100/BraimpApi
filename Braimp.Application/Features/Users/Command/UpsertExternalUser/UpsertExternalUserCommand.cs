using MediatR;

namespace Braimp.Application.Features.Users .Command.UpsertExternalUser;
public class UpsertExternalUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
