using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface IEnrollmentApiClient
    {
        Task<string> EnterEvent(EnrollmentModel model, string role);
    }
}
