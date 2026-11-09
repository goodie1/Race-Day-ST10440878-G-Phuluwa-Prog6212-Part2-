using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface IEventsApiClient
    {
        Task<string> CreateEvent(EventsModel m, int organiserId, string role);
        Task<string> GetEvent(int id);
        Task<string> UpdateEvent(int id, EventsModel model, string role);
        Task<string> DeleteEvent(int id, string role);
        Task<string> GetOrganiserEvents(int organiserId);
        Task<string> GetAllEvents();
    }
}
