using Braimp.Application;
using Braimp.Application.Abstraction;
using Braimp.Infrastructure;
using Braimp.WebApi.Extensions;
using Braimp.WebApi.Middleware;
using Braimp.WebApi.Services;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBraimpMappings();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddBraimpCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();


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
