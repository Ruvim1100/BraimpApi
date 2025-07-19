using System.Text.Json;

namespace Braimp.WebApi.Endpoints.User.UpsertUser;

public class PrincipalData
{
    public Dictionary<string, JsonElement> Principal { get; set; } = new Dictionary<string, JsonElement>();
}
