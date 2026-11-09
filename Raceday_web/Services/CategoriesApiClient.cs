using Microsoft.AspNetCore.WebUtilities;
using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public class CategoriesApiClient:ICategoriesApiClient
    {
        private readonly HttpClient _http;

        public CategoriesApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("RaceDayAPI");

        // Create a new category
        public async Task<string> CreateCategory(CategoriesModel m, string role)
        {
            var q = ApiQuery.Build(("category_type", m.Category_Type), ("distance", m.Distance),
                ("age", m.Age.ToString()), ("role", role));
            var r = await _http.PostAsync($"api/Categories?{q}", null);
            return await r.Content.ReadAsStringAsync();
        }

        public Task<string> GetCategory(int id) => _http.GetStringAsync($"api/Categories/{id}");

        // Get all categories
        public Task<string> GetAllCategories()
        {
            return _http.GetStringAsync("api/Categories");
        }
    }

}
