namespace Tradeflow.Application.Interfaces.Auth;

public interface ILoginService
{
    string Login(string email, string password);
}
