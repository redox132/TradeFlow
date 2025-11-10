namespace LatestEcommAPI.DTOs.User
{
    public class UserRegisterDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}