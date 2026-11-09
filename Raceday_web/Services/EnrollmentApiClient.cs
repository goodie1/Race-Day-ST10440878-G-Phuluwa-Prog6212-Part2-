using Microsoft.AspNetCore.WebUtilities;
using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public class EnrollmentApiClient:IEnrollmentApiClient
    {
        private readonly HttpClient _http;

        public EnrollmentApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("RaceDayAPI");

        // Enroll a participant in an event
        public async Task<string> EnterEvent(EnrollmentModel m, string role)
        {
            var q = ApiQuery.Build(("authentication_id", m.Authentication_Id.ToString()),
                ("category_id", m.Category_Id.ToString()), ("event_id", m.Event_Id.ToString()), ("role", role));
            var r = await _http.PostAsync($"api/Enrollment?{q}", null);
            return await r.Content.ReadAsStringAsync();
        }
    }
}
