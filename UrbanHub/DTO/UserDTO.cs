namespace UrbanHub.DTO
{
    public class UserDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime JoinDate { get; set; }

        public string Phone { get; set; } = null!;

    }
}
