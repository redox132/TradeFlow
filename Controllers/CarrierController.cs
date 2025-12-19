using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Tradeflow.DTOs.Carrier;
using Tradeflow.Helpers;
using Microsoft.AspNetCore.Authorization;
namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/carriers")]
public class CarrierController : ControllerBase
{
    // ============================================================
    // GET /api/carriers
    // ============================================================
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCarriers([FromHeader(Name = "X-API-KEY")] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return Unauthorized(new { message = "Missing API key" });

        int? userId = await UserHelper.GetRequestCallerUserId(apiKey);
        if (userId == null)
            return Unauthorized(new { message = "Invalid API key" });

        var carriers = new List<object>();

        using var connection = new SqliteConnection("Data Source=Data/db.db");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, name, code, tracking_url, supports_tracking, supports_international, is_active
            FROM carriers
            WHERE user_id = $user_id
        """;
        command.Parameters.AddWithValue("$user_id", userId);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            carriers.Add(new
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Code = reader.GetString(2),
                TrackingUrl = reader.IsDBNull(3) ? null : reader.GetString(3),
                SupportsTracking = reader.GetInt32(4) == 1,
                SupportsInternational = reader.GetInt32(5) == 1,
                IsActive = reader.GetInt32(6) == 1
            });
        }

        return Ok(new { message = "OK", carriers });
    }

    // ============================================================
    // POST /api/carriers
    // ============================================================
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCarrier(
        [FromHeader(Name = "X-API-KEY")] string apiKey,
        [FromBody] CreateCarrierDto dto)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return Unauthorized(new { message = "Missing API key" });

        int? userId = await UserHelper.GetRequestCallerUserId(apiKey);
        if (userId == null)
            return Unauthorized(new { message = "Invalid API key" });

        using var connection = new SqliteConnection("Data Source=Data/db.db");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO carriers (
                user_id, name, code, tracking_url, 
                supports_tracking, supports_international, is_active
            )
            VALUES ($user_id, $name, $code, $tracking_url, $supports_tracking, $supports_international, $is_active)
        """;

        command.Parameters.AddWithValue("$user_id", userId);
        command.Parameters.AddWithValue("$name", dto.Name);
        command.Parameters.AddWithValue("$code", dto.Code);
        command.Parameters.AddWithValue("$tracking_url", dto.TracingUrl ?? "");
        command.Parameters.AddWithValue("$supports_tracking", dto.SupportsTracking == "1" ? 1 : 0);
        command.Parameters.AddWithValue("$supports_international", dto.SupportInternational == "1" ? 1 : 0);
        command.Parameters.AddWithValue("$is_active", dto.IsActive == "1" ? 1 : 0);

        await command.ExecuteNonQueryAsync();

        return Ok(new { message = "Carrier created", data = dto });
    }

    // ============================================================
    // DELETE /api/carriers/{carrierId}
    // ============================================================
    [HttpDelete("{carrierId}")]
    [Authorize]
    public async Task<IActionResult> DeleteCarrier(
        [FromHeader(Name = "X-API-KEY")] string apiKey,
        [FromRoute] int carrierId)
    {
        int? userId = await UserHelper.GetRequestCallerUserId(apiKey);

        if (userId == null)
            return Unauthorized(new { message = "Invalid API key" });

        using var connection = new SqliteConnection("Data Source=Data/db.db");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText =
            "DELETE FROM carriers WHERE id = $id AND user_id = $user_id";

        command.Parameters.AddWithValue("$id", carrierId);
        command.Parameters.AddWithValue("$user_id", userId);

        int rows = await command.ExecuteNonQueryAsync();

        if (rows == 0)
            return NotFound(new { message = "Carrier not found or not owned by this user" });

        return Ok(new { message = "Deleted" });
    }
}
