using System.ComponentModel.DataAnnotations;

namespace RaceDay_web.Models
{
    public class CategoriesModel
    {
        public int Category_Id { get; set; }
        [Required, Display(Name = "Category type")] public string Category_Type { get; set; } = "";
        [Required] public string Distance { get; set; } = "";
        [Range(1, 120)] public int Age { get; set; }
    }
}
