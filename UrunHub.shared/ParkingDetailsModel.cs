using UrbanHub.Entities;

namespace UrbanHub.shared;

public class ParkingDetailsModel
{
    public ParkingSpace? ParkingSpaces { get; set; } 
    public List<ParkingBooking>? ParkingBookings { get; set; }
    public ParkingBookingDTO ? ParkingBookingDTO { get; set; }

}