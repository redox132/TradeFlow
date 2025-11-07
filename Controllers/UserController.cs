using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Test.Models;

namespace Test.Controllers;

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
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Email = reader.GetString(1),
                    Name = reader.GetString(2)
                });
            }

            return Ok(new { message = "200", users = users });
        }

    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(new { message = $"Fetched user {id}" });
    }

    [HttpPost]
    public IActionResult CreateUser(User user) // use fake response
    {
        using (var connection = new SqliteConnection("Data source=Data/db.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO user (name, password, email) VALUES (@name, @password, @email)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);

            command.ExecuteNonQuery();

            user.Id = (int)connection.LastInsertRowId;
            
            return Ok(new { message = "200", user = user });
        }
    }
}
