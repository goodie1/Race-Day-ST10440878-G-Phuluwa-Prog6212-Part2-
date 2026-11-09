using Raceday_web.Models;
using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface IAuthenticationApiClient
    {
        Task<string> SignUp(AuthenticationModel model);
        Task<string> Login(LoginModel model);
        Task<string> GetProfile(int id);
        Task<string> UpdateProfile(int id, AuthenticationModel model);
        Task<string> DeleteProfile(int id, string role);

    }
}
