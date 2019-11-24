using System.Threading.Tasks;
using TasksSpa.Model;

namespace TasksSpa.Services.Contracts
{
    public interface IAuthorizationApi
    {
        Task<string> Login(LoginParams loginParameters);
        Task Register(RegistrationParams registerParameters);
        Task Logout(string token);
        Task<UserDto> GetUserInfo(string token);
    }
}
