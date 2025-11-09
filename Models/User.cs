// Models/User.cs
namespace LatestEcommAPI.Models;

public class User
{
    public string? Name { get; set; }
    public required string Email { get; set; }

    public required string Password { get; set; }
}
