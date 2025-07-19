namespace Braimp.WebApi.Endpoints.User.UpsertUser;
public class Request
{
    public string Version { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Trigger { get; set; } = string.Empty;
    public PrincipalData Data { get; set; } = null!;
}
