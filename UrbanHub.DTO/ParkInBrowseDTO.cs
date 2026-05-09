namespace UrbanHub.DTO;

public class ParkINBrowseDTO
{
    public SearchParkingSpace SearchSpaces { get; set; } = new();
    public List<ParkingSpaceDTO> ParkingSpaces { get; set; } = new();

}