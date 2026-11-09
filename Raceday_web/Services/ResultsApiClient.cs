using Microsoft.AspNetCore.WebUtilities;
using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public class ResultsApiClient:IResultsApiClient
    {
        private readonly HttpClient _http;

        public ResultsApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("RaceDayAPI");

        public async Task<string> EnterResult(ResultsModel m, string role)
        {
            var q = Query(m, role);
            var r = await _http.PostAsync($"api/Result?{q}", null);
            return await r.Content.ReadAsStringAsync();
        }

        public Task<string> GetResult(int id) => _http.GetStringAsync($"api/Result/{id}");

        public async Task<string> UpdateResult(int id, ResultsModel m, string role)
        {
            var r = await _http.PutAsync($"api/Result/{id}?{Query(m, role)}", null);
            return await r.Content.ReadAsStringAsync();
        }

        private static string Query(ResultsModel m, string role) => ApiQuery.Build(
            ("finish_time", m.Finish_Time), ("race_Positions", m.Race_Positions),
            ("Avg_pace", m.Avg_Pace), ("role", role));

        public Task<string> GetAllResults()
        {
            return _http.GetStringAsync("api/Result");
        }
    }
}

