using System.ComponentModel.DataAnnotations;

namespace Raceday_web.Models
{
    public class AuthenticationModel
    {
        public int Authentication_Id { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = "";

        [Required, StringLength(60)]
        public string Surname { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, Display(Name = "Cell number")]
        public string Cell_Number { get; set; } = "";

        [Required]
        public string Role { get; set; } = "Participant";

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = ""; 
    }
}
