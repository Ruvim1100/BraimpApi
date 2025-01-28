using Braimp.Application;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Interfaces;
using Braimp.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(configuration =>
    {
        configuration.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        configuration.AddProfile(new AssemblyMappingProfile(typeof(IBraimpDbContext).Assembly));
    });

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<BraimpDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
        throw;
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
