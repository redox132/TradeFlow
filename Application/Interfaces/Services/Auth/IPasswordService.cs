namespace Tradeflow.Application.Interfaces.Services.Auth;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}