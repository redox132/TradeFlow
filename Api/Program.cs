

using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Tradeflow.Api.Extensions;
using Tradeflow.Application.Interfaces.Auth;
using Tradeflow.Application.Services.Auth;
using Tradeflow.Infrastructure.Auth;
using Tradeflow.Infrastructure.Data;
using Tradeflow.Infrastructure.Repositories;
using Tradeflow.Application.Interfaces.Repositories;

Env.Load();
Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Tradeflow API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Enter: Bearer {token}"
    });

    // Note: Security requirement configuration is omitted here to avoid
    // assembly-type mismatches across Microsoft.OpenApi versions. If you
    // need the Swagger UI to require a Bearer token, add the requirement
    // after confirming the Microsoft.OpenApi package version in the Api
    // project's dependencies.
});

// DI
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddJwt(builder.Configuration);

// PostgreSQL
var connString =
    $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
app.Run($"http://localhost:{port}");
