using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LatestEcommAPI.DTOs.User;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LatestEcommAPI.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] UserRegisterDto userDto)
    {
        try
        {
            // Hash the password
            string hashedPassword = HashPassword(userDto.Password);

            using (var connection = new SqliteConnection("Data Source=Data/db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO user (email, password, name)
                    VALUES ($email, $password, $name)";
                command.Parameters.AddWithValue("$email", userDto.Email);
                command.Parameters.AddWithValue("$password", hashedPassword);
                command.Parameters.AddWithValue("$name", userDto.Name);

                command.ExecuteNonQuery();
            }

            return Ok(new { message = "User has been created!" });
        }
        catch (SqliteException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    private string HashPassword(string password)
    {
        // Generate a salt
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        // Derive the hash
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Store salt + hash together
        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }
}
