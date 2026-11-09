using RaceDay_web.Models;

namespace RaceDay_web.Services
{
    public class ProfileApiClient:IProfileApiClient
    {
        private readonly IWebHostEnvironment _environment;
        public ProfileApiClient(IWebHostEnvironment environment) => _environment = environment;

        public async Task<string> UploadProfilePicture(ProfileModel model)
        {
            var file = model.ProfilePicture;
            if (file is null || file.Length == 0) return "Please select an image.";
            if (file.Length > 2 * 1024 * 1024) return "Image must be 2MB or smaller.";

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) return "Only JPG, PNG or WEBP files are allowed.";

            var folder = Path.Combine(_environment.WebRootPath, "uploads", "profiles");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var path = Path.Combine(folder, fileName);

            await using var stream = new FileStream(path, FileMode.CreateNew);
            await file.CopyToAsync(stream);
            return $"/uploads/profiles/{fileName}";
        }
    }
}
