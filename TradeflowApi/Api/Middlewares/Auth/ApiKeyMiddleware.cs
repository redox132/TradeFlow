// namespace Tradeflow.TradeflowApi.Api.Middlewares.Auth;

// using Tradeflow.TradeflowApi.Application.Interfaces.Services.Auth;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Routing;

// public class ApiKeyMiddleware
// {
//     private readonly RequestDelegate _next;

//     public ApiKeyMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(HttpContext context, IApiKeyService apiKeyService)
//     {
//         // If endpoint allows anonymous access, skip API key check
//         var endpoint = context.GetEndpoint();
//         if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
//         {
//             await _next(context);
//             return;
//         }

//         if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
//         {
//             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//             return;
//         }

//         // Retrieve the API key from DB (including Seller)
//         var apiKeyEntity = await apiKeyService.GetApiKey(apiKey!);

//         if (apiKeyEntity == null)
//         {
//             // Key does not exist or is revoked
//             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
//             return;
//         }

//         // Optionally, store Seller info in context for controllers
//         context.Items["Seller"] = apiKeyEntity.Seller;

//         await _next(context);
//     }

// }
