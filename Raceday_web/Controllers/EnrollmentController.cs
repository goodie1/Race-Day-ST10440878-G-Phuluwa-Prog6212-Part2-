using Microsoft.AspNetCore.Mvc;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class EnrollmentController : Controller
{
    private readonly IEnrollmentApiClient _service;
    public EnrollmentController(IEnrollmentApiClient service) => _service = service;
    
    [HttpGet] public IActionResult Create() => View(new EnrollmentModel());
    
    [HttpPost]
    public async Task<IActionResult> Create(EnrollmentModel model)
    {
        if (!ModelState.IsValid) return View(model);
        TempData["Message"] = await _service.EnterEvent(model, HttpContext.Session.GetString("Role") ?? "");
        return RedirectToAction("Dashboard", "Home");
    }
}