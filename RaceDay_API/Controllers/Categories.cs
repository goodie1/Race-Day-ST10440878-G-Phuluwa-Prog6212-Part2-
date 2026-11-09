using Microsoft.AspNetCore.Mvc;
using RaceDay_API.Services;

namespace RaceDay_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly ICategoriesServices _categoriesService;

        public Categories(ICategoriesServices categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public string category(string category_type, string distance, int age, string role)
        {
            return _categoriesService.category(
                category_type, distance, age, role);
        }

        [HttpGet("{id}")]
        public string return_info(int id)
        {
            return _categoriesService.return_info(id);
        }

        [HttpGet]
        public string get_all_categories()
        {
            return _categoriesService.get_all_categories();
        }
    }
}