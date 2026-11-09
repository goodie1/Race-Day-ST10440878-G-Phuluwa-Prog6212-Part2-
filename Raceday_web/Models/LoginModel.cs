using System.ComponentModel.DataAnnotations;


namespace RaceDay_web.Models
{
    public class LoginModel
    {
        [Required] public string Name { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    
    }
}
