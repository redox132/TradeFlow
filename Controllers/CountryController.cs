using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Mvc;
using LatestEcommAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCountries()
    {
        using (var connection = new SqliteConnection("Data Source=Data/db.db"))
        {
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM supported_countries";

            var countries = new List<object>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    countries.Add(new CountryDto
                    {
                        CountryId = reader.GetInt32(0),
                        CountryName = reader.GetString(1),
                        CountryCode = reader.GetString(2),
                        IsActive = Convert.ToBoolean(reader.GetInt32(3))
                    });
                }
            }
            return Ok(new { message = "OK", countries });
        }
    }
}