using System.ComponentModel.DataAnnotations;

namespace UrbanHub.DTO
{
    public class SearchParkingSpace
    {
        //[validatrenever]
        public DateTime? DateAndTime { get; set; } 

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        public string SearchText { get; set; } = null!;

    }
}
