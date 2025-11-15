using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.Models;
using Microsoft.AspNetCore.Authorization;
using LatestEcommAPI.DTOs.User;
using Microsoft.AspNetCore.Http.Headers;


namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("whoami")]
    public async Task<IActionResult> WhoAmI([FromHeader] string ApiKey)
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM user where ApiKey = $ApiKey";
            command.Parameters.AddWithValue("$ApiKey", ApiKey);

            if (!Request.Headers.ContainsKey("ApiKey"))
            {
                return BadRequest("Where is ApiKey?");
            }

            var user = new List<object>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    user.Add(new UserResponseDto
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                    });
                }
            }
            if (user.Count == 0)
            {
                return NotFound(new
                {
                    errors = new
                    {
                        statusIsUnfortunately = 404,
                        description = "How? How can you reach this point and the user does not exist?"
                    }
                });
            }
            return Ok(new { message = "Ok", user });
        }
    }
}
