using Braimp.Ai;
using Braimp.Application;
using Braimp.Infrastructure;
using Braimp.Identity;
using Braimp.WebApi.Configuration;
using Braimp.WebApi.Extensions;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBraimpMappings()
    .AddApplication()
    .AddIdentity(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddAi(builder.Configuration)
    .AddBraimpCors()
    .AddEndpointsApiExplorer()
    .AddCarter(configurator: c => c.WithValidatorLifetime(ServiceLifetime.Scoped))
    .AddBraimpSwagger(builder.Configuration);

var app = builder.Build();

await app.InitializeDatabaseAsync();
app.UseSwagger(c => c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Braimp API V1");
    c.OAuthClientId("b4f7e9d9-93d9-488b-ba6e-c4f34d5abf47");
    c.OAuthUsePkce();
});
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
app.Run();