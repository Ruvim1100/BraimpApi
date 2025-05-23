using Carter;

namespace Braimp.WebApi.Endpoints.Claims;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Claims.AddClaims, Handler);
    }

    private async Task<IResult> Handler(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
