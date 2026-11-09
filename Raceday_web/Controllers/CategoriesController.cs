using Microsoft.AspNetCore.Mvc;
using RaceDay_web.Models;
using RaceDay_web.Services;

namespace RaceDay_web.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoriesApiClient _service;
    public CategoriesController(ICategoriesApiClient service) => _service = service;
  
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string result = await _service.GetAllCategories();

        var categories = System.Text.Json.JsonSerializer.Deserialize<List<CategoriesModel>>(
            result,
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return View(categories ?? new List<CategoriesModel>());
    }
    [HttpGet] public IActionResult Create() => View(new CategoriesModel());
    [HttpPost]
    public async Task<IActionResult> Create(CategoriesModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string role = HttpContext.Session.GetString("Role") ?? "";

        if (!role.Equals("Organizer", StringComparison.OrdinalIgnoreCase))
        {
            TempData["Error"] = "Only organisers can create categories.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Message"] = await _service.CreateCategory(model, role);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewBag.CategoryDetails = await _service.GetCategory(id);
        return View("Index");
    }
}