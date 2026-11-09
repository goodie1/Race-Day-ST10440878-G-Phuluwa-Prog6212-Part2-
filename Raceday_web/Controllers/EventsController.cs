using Microsoft.AspNetCore.Mvc;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class EventsController : Controller
{
    private readonly IEventsApiClient _service;
    public EventsController(IEventsApiClient service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        string role = HttpContext.Session.GetString("Role") ?? "";

        if (userId == null)
            return RedirectToAction("Login", "Authentication");

        string result;

        if (role.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
        {
            result = await _service.GetOrganiserEvents(userId.Value);
        }
        else
        {
            result = await _service.GetAllEvents();
        }

        var events = System.Text.Json.JsonSerializer.Deserialize<List<EventsModel>>(
            result,
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return View(events ?? new List<EventsModel>());
    }
    [HttpGet] public IActionResult Create() => View(new EventsModel());
    
    [HttpPost]
    public async Task<IActionResult> Create(EventsModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        int? organiserId = HttpContext.Session.GetInt32("UserId");
        string role = HttpContext.Session.GetString("Role") ?? "";

        if (organiserId == null)
        {
            return RedirectToAction("Login", "Authentication");
        }

        if (!role.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
        {
            TempData["Error"] = "Only organisers can create events.";
            return RedirectToAction(nameof(Index));
        }

        string result = await _service.CreateEvent(model, organiserId.Value, role);

        TempData["Message"] = result;

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewBag.EventDetails = await _service.GetEvent(id);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        TempData["Message"] = await _service.DeleteEvent(id, HttpContext.Session.GetString("Role") ?? "");
        return RedirectToAction(nameof(Index));
    }
}
