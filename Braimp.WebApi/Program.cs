using Braimp.Ai;
using Braimp.Application;
using Braimp.Identity;
using Braimp.Infrastructure;
using Braimp.WebApi.Configuration;
using Braimp.WebApi.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBraimpMappings()
    .AddApplication()
    .AddIdentity(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddAi(builder.Configuration)
    .AddBraimpCors(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddCarter(configurator: c => c.WithValidatorLifetime(ServiceLifetime.Scoped))
    .AddBraimpSwagger(builder.Configuration);

var app = builder.Build();

await app.InitializeDatabaseAsync();
app.UseSwagger(c => c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Braimp API V1");
    c.OAuthClientId("5e947c18-aec6-4cf8-b4c8-b442ddfb7ab2");
    c.OAuthUsePkce();
});
app.UseTimig();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.MapGet("/", () => Results.Ok("Braimp API is running."));
app.Run();