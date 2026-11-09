using Microsoft.AspNetCore.Mvc;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileApiClient _service;
    public ProfileController(IProfileApiClient service) => _service = service;

    [HttpGet]
    public IActionResult Index() => View(new ProfileModel
    {
        Name = HttpContext.Session.GetString("UserName") ?? "",
        ProfileImage = HttpContext.Session.GetString("ProfileImage")
    });

    [HttpPost]
    public async Task<IActionResult> Upload(ProfileModel model)
    {
        var result = await _service.UploadProfilePicture(model);
        if (result.StartsWith("/uploads/profiles/"))
        {
            HttpContext.Session.SetString("ProfileImage", result);
            TempData["Message"] = "Profile picture uploaded successfully.";
        }
        else TempData["Error"] = result;
        return RedirectToAction(nameof(Index));
    }
}