using System.ComponentModel.DataAnnotations;

namespace UrbanHub.DTO
{
    public class UserDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters,\n include uppercase, lowercase, number and special character.")]
        public string Password { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime JoinDate { get; set; }

        public string Phone { get; set; } = null!;

    }
}
