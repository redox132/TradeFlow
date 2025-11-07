using LatestEcommAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace LatestEcommAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=Data/db.db"))
                {
                    connection.Open();

                    // Check if the user exists in the database
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM user WHERE email = $email AND password = $password";
                    command.Parameters.AddWithValue("$email", user.Email);
                    command.Parameters.AddWithValue("$password", user.Password);
                    var count = (long?)command.ExecuteScalar();

                    if (count == 0)
                    {
                        return Unauthorized(new { error = "Invalid email or password" });
                    }
                }

                // If we reach this point, the user is authenticated
                return Ok(new { message = "Login successful" });
            }
            catch (SqliteException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
             
