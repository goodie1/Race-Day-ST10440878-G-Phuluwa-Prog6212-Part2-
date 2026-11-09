using Microsoft.AspNetCore.Mvc;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class ResultsController : Controller
{
    private readonly IResultsApiClient _service;
    public ResultsController(IResultsApiClient service) => _service = service;
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Login", "Authentication");

        string result = await _service.GetAllResults();

        try
        {
            var results = System.Text.Json.JsonSerializer.Deserialize<List<ResultsModel>>(
                result,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(results ?? new List<ResultsModel>());
        }
        catch
        {
            TempData["Error"] = "Unable to load race results: " + result;
            return View(new List<ResultsModel>());
        }
    }
    [HttpGet] public IActionResult Create() => View(new ResultsModel());
    [HttpPost]
    public async Task<IActionResult> Create(ResultsModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string role = HttpContext.Session.GetString("Role") ?? "";

        if (!role.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
        {
            TempData["Error"] = "Only organisers can enter race results.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Message"] = await _service.EnterResult(model, role);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewBag.ResultDetails = await _service.GetResult(id);
        return View("Index");
    }
}
