using Microsoft.AspNetCore.WebUtilities;
using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public class EventsApiClient :IEventsApiClient
    {
        private readonly HttpClient _http;

        public EventsApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("RaceDayAPI");

        public async Task<string> CreateEvent(EventsModel m, int organiserId, string role)
        {
            var q = Query(m, organiserId, role);

            return await Read(
                await _http.PostAsync($"api/Events?{q}", null)
            );
        }

        public Task<string> GetEvent(int id) => _http.GetStringAsync($"api/Events/{id}");

        public async Task<string> UpdateEvent(int id, EventsModel m, string role)
        {
            var q = ApiQuery.Build(
                ("event_name", m.Event_Name),
                ("date", m.Date),
                ("time", m.Time),
                ("event_Type", m.Event_Type),
                ("Event_description", m.Event_Description),
                ("location", m.Location),
                ("race_route", m.Race_Route),
                ("weather", m.Weather),
                ("role", role)
            );

            return await Read(await _http.PutAsync($"api/Events/{id}?{q}", null));
        }

        public async Task<string> DeleteEvent(int id, string role) =>
            await Read(await _http.DeleteAsync($"api/Events/{id}?{ApiQuery.Build(("role", role))}"));

        private static string Query(EventsModel m, int organiserId, string role) =>
      ApiQuery.Build(
          ("event_name", m.Event_Name),
          ("date", m.Date),
          ("time", m.Time),
          ("event_Type", m.Event_Type),
          ("Event_description", m.Event_Description),
          ("location", m.Location),
          ("race_route", m.Race_Route),
          ("weather", m.Weather),
          ("organiser_id", organiserId.ToString()),
          ("role", role)
      );

        private static async Task<string> Read(HttpResponseMessage r)
        {
            var body = await r.Content.ReadAsStringAsync();
            return r.IsSuccessStatusCode ? body : $"API error ({(int)r.StatusCode}): {body}";
        }

        public async Task<string> GetOrganiserEvents(int organiserId)
        {
            return await _http.GetStringAsync(
                $"api/Events/organiser/{organiserId}");
        }

        public async Task<string> GetAllEvents()
        {
            return await _http.GetStringAsync("api/Events");
        }
    }


}

