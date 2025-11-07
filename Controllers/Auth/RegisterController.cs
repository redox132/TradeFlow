using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Test.Models;

namespace Test.Auth.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] User user)
    {

        try
        {
            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                connection.Open();

                // Insert the new user into the database
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO user (email, password, name) VALUES ($email, $password, $name)";
                command.Parameters.AddWithValue("$email", user.Email);
                command.Parameters.AddWithValue("$password", user.Password);
                command.Parameters.AddWithValue("$name", user.Name);
                command.ExecuteNonQuery();
            }

            return Ok(new { message = user.Name });
        }
        catch (SqliteException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}