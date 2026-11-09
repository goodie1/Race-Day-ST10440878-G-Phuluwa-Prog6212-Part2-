using Microsoft.AspNetCore.WebUtilities;
using Raceday_web.Models;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Services
{
    public class AuthenticationApiClient : IAuthenticationApiClient
    {
        private readonly HttpClient _http;

        public AuthenticationApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("RaceDayAPI");

        // Register a new user
        public async Task<string> SignUp(AuthenticationModel m)
        {
            var q = ApiQuery.Build(("name", m.Name), ("surname", m.Surname), ("email", m.Email),
                ("Cell_Number", m.Cell_Number), ("role", m.Role), ("password", m.Password));
            return await Read(await _http.PostAsync($"api/Authentication/Sign%20up?{q}", null));
        }

        public async Task<string> Login(LoginModel m)
        {
            var q = ApiQuery.Build(("name", m.Name), ("email", m.Email), ("password", m.Password));
            return await Read(await _http.PostAsync($"api/Authentication/Login?{q}", null));
        }

        public Task<string> GetProfile(int id) => _http.GetStringAsync($"api/Authentication/{id}");

        public async Task<string> UpdateProfile(int id, AuthenticationModel m)
        {
            var q = ApiQuery.Build(("name", m.Name), ("surname", m.Surname), ("email", m.Email),
                ("Cell_Number", m.Cell_Number), ("password", m.Password));
            return await Read(await _http.PutAsync($"api/Authentication/{id}?{q}", null));
        }

        public async Task<string> DeleteProfile(int id, string role) =>
            await Read(await _http.DeleteAsync($"api/Authentication/{id}?{ApiQuery.Build(("role", role))}"));

        private static async Task<string> Read(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? body : $"API error ({(int)response.StatusCode}): {body}";
        }
    }
}
