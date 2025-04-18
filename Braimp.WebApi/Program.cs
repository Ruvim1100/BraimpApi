using Braimp.Application;
using Braimp.Infrastructure;
using Braimp.WebApi.Configuration;
using Braimp.WebApi.Extensions;
using Braimp.WebApi.Services;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBraimpMappings()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddBraimpCors()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCarter();

var app = builder.Build();

await app.InitializeDatabaseAsync();
app.UseSwagger(c => c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
app.UseSwaggerUI();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.MapCarter();
app.Run();
