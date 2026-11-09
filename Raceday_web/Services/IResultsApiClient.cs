using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface IResultsApiClient
    {
        Task<string> EnterResult(ResultsModel model, string role);

        Task<string> GetResult(int id);

        Task<string> UpdateResult(int id, ResultsModel model, string role);

        Task<string> GetAllResults();
    }
}