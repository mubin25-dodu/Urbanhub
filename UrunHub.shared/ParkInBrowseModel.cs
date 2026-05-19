using UrbanHub.DTO;

namespace UrbanHub.shared;

public class ParkInBrowseModel
{
    public SearchParkingSpace? SearchSpaces { get; set; }
    public List<ParkingSpaceDTO> ParkingSpaces { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalResults { get; set; }
}