using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.Models;

namespace LatestEcommAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
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
    public IActionResult GetUser(int id)
    {
        return Ok(new { message = $"Fetched user {id}" });
    }

    [HttpPost]
    public IActionResult CreateUser() // use fake response
    {
        return Ok(new { message = "User created!", user = new { id = 1, name = "John Doe" } });
    }

}
