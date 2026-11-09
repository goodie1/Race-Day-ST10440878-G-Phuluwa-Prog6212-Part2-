using System.ComponentModel.DataAnnotations;

namespace RaceDay_web.Models
{
    public class EnrollmentModel
    {
        [Range(1, int.MaxValue)] public int Authentication_Id { get; set; }
        [Range(1, int.MaxValue)] public int Category_Id { get; set; }
        [Range(1, int.MaxValue)] public int Event_Id { get; set; }
    
    }
}
