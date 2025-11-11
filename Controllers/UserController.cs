using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult GetAllUsers()
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM user";

            command.ExecuteNonQuery();

            var reader = command.ExecuteReader();
            var users = new List<object>();
            while (reader.Read())
            {
                users.Add(new
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Name = reader.GetString(2)
                });
            }

            return Ok(new { message = "200", users });
        }

    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetUser(int id)
    {
        return Ok(new { message = $"Fetched user {id}" });
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateUser() 
    {
        return Ok(new { message = "User created!", user = new { id = 1, name = "John Doe" } });
    }

}
