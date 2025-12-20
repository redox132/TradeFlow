using Tradeflow.Application.DTOs;
using Tradeflow.Application.Interfaces;

namespace Tradeflow.Application.Services.Auth
{
    public class LoginService : ILoginService
    {
        public TestDTO Get(TestDTO testDTO)
        {
            testDTO.Name = "s";
            return testDTO;
        }
    }
}
