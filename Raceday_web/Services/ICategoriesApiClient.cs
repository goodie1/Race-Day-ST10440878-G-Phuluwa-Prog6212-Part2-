using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface ICategoriesApiClient
    {
        Task<string> CreateCategory(CategoriesModel model, string role);

        Task<string> GetCategory(int id);

        Task<string> GetAllCategories();
    }
}