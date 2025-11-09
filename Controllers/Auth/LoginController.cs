using LatestEcommAPI.User.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LatestEcommAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly string _jwtSecretKey;


        public LoginController(IConfiguration configuration)
        {
            _jwtSecretKey = configuration["Jwt:Key"];
        }
        private string JwtSecretKey => _jwtSecretKey;

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                string? storedHash = null;

                using (var connection = new SqliteConnection("Data Source=Data/db.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT password FROM user WHERE email = $email";
                    command.Parameters.AddWithValue("$email", loginDto.Email);

                    using var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        storedHash = reader.GetString(0);
                    }
                }

                if (storedHash == null || !VerifyPassword(loginDto.Password, storedHash))
                {
                    return Unauthorized(new { error = "Invalid email or password" });
                }

                // Generate JWT token
                var token = GenerateJwtToken(loginDto.Email);

                return Ok(new { message = "Login successful", token });
            }
            catch (SqliteException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var hashedInput = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return CryptographicOperations.FixedTimeEquals(hashedInput, hash);
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
