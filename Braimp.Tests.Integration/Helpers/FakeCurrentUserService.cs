using Braimp.Application.Abstraction;

namespace Braimp.Tests.Integration.Helpers;
public class FakeCurrentUserService : ICurrentUserService
{
    public Guid UserId { get; }
    public FakeCurrentUserService(Guid userId) => UserId = userId;
}
