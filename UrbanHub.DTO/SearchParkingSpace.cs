using System.ComponentModel.DataAnnotations;

namespace UrbanHub.DTO
{
    public class SearchParkingSpace
    {

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string SearchText { get; set; } = null!;

    }
}
