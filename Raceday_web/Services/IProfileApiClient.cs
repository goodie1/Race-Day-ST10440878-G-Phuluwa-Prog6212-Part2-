using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public interface IProfileApiClient
    {
        Task<string> UploadProfilePicture(ProfileModel model);
    }
}
