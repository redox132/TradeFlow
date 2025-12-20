using DotNetEnv;
using Tradeflow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Tradeflow.Application.Interfaces;
using Tradeflow.Application.Services.Auth;

// Load .env variables
Env.Load();
Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<ILoginService, LoginService>();

// Build PostgreSQL connection string from .env
var connString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                 $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                 $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                 $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                 $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connString));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

// Use PORT from .env (fallback to 3000)
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
app.Run($"http://localhost:{port}");
