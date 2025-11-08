using LatestEcommAPI.Migrations;
using Microsoft.OpenApi.Models; // <-- required for OpenApiInfo
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add controller support
builder.Services.AddControllers();

// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LatestEcommAPI",
        Version = "v1"
    });
});

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LatestEcommAPI v1");
});

// Map all controllers
app.MapControllers();

// Run DB migrations
DbController.Migrate();

app.Run();
