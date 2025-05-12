using Braimp.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Braimp.Infrastructure.Identity
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid UserId
        {
            get
            {
                var user = httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return Guid.Empty;

                var oid = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value
                          ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(oid, out var id) ? id : Guid.Empty;
            }
        }
    }
}
