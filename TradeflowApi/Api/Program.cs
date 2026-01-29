using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Application.Services.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Repositories.Auth;
using Tradeflow.TradeflowApi.Application.Interfaces.Services;
using Tradeflow.TradeflowApi.Infrastructure.Repositories;
using Tradeflow.TradeflowApi.Application.Services.Auth;
using Tradeflow.TradeflowApi.Api.Middlewares.Auth;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using DotNetEnv;

Env.Load();
Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Tradeflow API",
        Version = "v1"
    });

    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.OpenApiSecurityScheme
    { 
        Description = "API Key needed to access the endpoints. Example: X-API-KEY: {your_api_key}",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Name = "X-API-KEY",
        Type = Microsoft.OpenApi.SecuritySchemeType.ApiKey
    });
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
builder.Services.AddScoped<IOrderShipmentService, OrderShipmentService>();
builder.Services.AddScoped<IOrderShipmentRepository, OrderShipmentRepository>();
builder.Services.AddScoped<IShipmentMethodService, ShipmentMethodService>();
builder.Services.AddScoped<IShipmentMethodRepository, ShipmentMethodRepository>();
// Returns / Storages / Suppliers
builder.Services.AddScoped<IOrderReturnService, OrderReturnService>();
builder.Services.AddScoped<IOrderReturnRepository, OrderReturnRepository>();

builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IStorageRepository, StorageRepository>();

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();


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

// app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();


app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
app.Run($"http://localhost:{port}");
