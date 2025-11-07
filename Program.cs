using LatestEcommAPI.Database;

var builder = WebApplication.CreateBuilder(args);

// Add this line to register controller support
builder.Services.AddControllers();

var app = builder.Build();

// Maps all controller endpoints (like /api/users)
app.MapControllers();

// Run your DB migration logic
DbController.Migrate();

app.Run();
