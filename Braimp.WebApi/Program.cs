using Braimp.Application;
using Braimp.Application.Abstraction;
using Braimp.Infrastructure;
using Braimp.Infrastructure.Identity;
using Braimp.WebApi.Configuration;
using Braimp.WebApi.Extensions;
using Braimp.WebApi.Services;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBraimpMappings()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddBraimpCors()
    .AddEndpointsApiExplorer()
    .AddCarter();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://login.microsoftonline.com/cefef5ae-55dc-4048-a12a-955716d722e5/oauth2/v2.0/authorize"),
                TokenUrl = new Uri("https://login.microsoftonline.com/cefef5ae-55dc-4048-a12a-955716d722e5/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "api://053d3b2f-b061-462c-8d14-7a158f0dcc27/access_as_user", "Access API" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "api://053d3b2f-b061-462c-8d14-7a158f0dcc27/access_as_user" }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(
        builder.Configuration.GetSection("AzureAd"),
        jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme,
        subscribeToJwtBearerMiddlewareDiagnosticsEvents: false
    );

builder.Services.Configure<JwtBearerOptions>(
    JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", p => p.RequireRole("User", "Admin"));
    options.AddPolicy("Admin", p => p.RequireRole("Admin"));
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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