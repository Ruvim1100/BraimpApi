using Braimp.Application.Abstraction;

namespace IntegrationTests;
public class TestCurrentUserService : ICurrentUserService
{
    public Guid UserId => Guid.Parse("5165B340-3735-449B-98AE-129A0A181F7A");

}
