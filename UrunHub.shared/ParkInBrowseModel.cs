using UrbanHub.DTO;

namespace UrbanHub.shared;

public class ParkInBrowseModel
{
    public SearchParkingSpace SearchSpaces { get; set; } = new();
    public List<ParkingSpaceDTO> ParkingSpaces { get; set; } = new();

}