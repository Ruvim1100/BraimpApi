using Braimp.Application.Features.Users.Command.UpsertExternalUser;
using Braimp.Domain.Entities.Users.Enums;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Braimp.WebApi.Endpoints.User.UpsertUser;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Users.UpsertUser, Handler)
            .RequireAuthorization("AuthExtension");
    }

    private static async Task<IResult> Handler(
        [FromBody] Request request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var principal = request.Data.Principal;

        if (!principal.TryGetValue("id", out var idElem) ||
            idElem.ValueKind != JsonValueKind.String ||
            !Guid.TryParse(idElem.GetString(), out var userId))
        {
            return Results.BadRequest("Invalid or missing 'id'");
        }

        var command = new UpsertExternalUserCommand
        {
            Id = userId,
            Email = TryGet(principal, "email"),
            Name = TryGet(principal, "name"),
            Surname = TryGet(principal, "family_name"),
            GivenName = TryGet(principal, "given_name"),
            Country = TryGet(principal, "country")
        };

        await mediator.Send(command, cancellationToken);

        return Results.Ok(new
        {
            actions = new[]
            {
                new
                {
                    type = "ProvideClaimsForToken",
                    claims = new[]
                    {
                        new { id = "role", value = AppRole.User }
                    }
                }
            }
        });
    }

    private static string TryGet(Dictionary<string, JsonElement> principal, string key)
    {
        return principal.TryGetValue(key, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString() ?? string.Empty
            : string.Empty;
    }
}
