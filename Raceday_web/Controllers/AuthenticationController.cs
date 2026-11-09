using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Raceday_web.Models;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthenticationApiClient _service;
    public AuthenticationController(IAuthenticationApiClient service) => _service = service;

    [HttpGet] public IActionResult Register() => View(new AuthenticationModel());
    [HttpPost]
    public async Task<IActionResult> Register(AuthenticationModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _service.SignUp(model);
        TempData["Message"] = result;
        return RedirectToAction("Login");
    }

    [HttpGet] public IActionResult Login() => View(new LoginModel());
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var response = await _service.Login(model);
        try
        {
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement;
            if (root.TryGetProperty("success", out var success) && success.GetBoolean())
            {
                HttpContext.Session.SetInt32("UserId", root.GetProperty("authentication_id").GetInt32());
                HttpContext.Session.SetString("UserName", root.GetProperty("name").GetString() ?? "");
                HttpContext.Session.SetString("Role", root.GetProperty("role").GetString() ?? "");
                return RedirectToAction("Dashboard", "Home");
            }
            ModelState.AddModelError("", root.TryGetProperty("message", out var msg) ? msg.GetString() ?? "Login failed." : "Login failed.");
        }
        catch
        {
            ModelState.AddModelError("", "The API returned an unexpected login response. Check the API response and try again.");
        }
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}