using System.ComponentModel.DataAnnotations;

namespace RaceDay_web.Models
{
    public class ProfileModel
    {
        public int Authentication_Id { get; set; }
        [Required] public string Name { get; set; } = "";
        [Required] public string Surname { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        public string Cell_Number { get; set; } = "";
        public string? ProfileImage { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
