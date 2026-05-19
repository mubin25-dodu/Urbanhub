using AutoMapper;
using UrbanHub.DTO;
using UrbanHub.Entities;

namespace UrbanHub.shared
{
    public class mapper : Profile
    {
        public mapper()
        {
            // ParkingSpace mappings
            CreateMap<ParkingSpace, ParkingSpaceDTO>();
            CreateMap<ParkingSpaceDTO, ParkingSpace>();

            // ParkingBooking mappings
            CreateMap<ParkINBooking, ParkingBookingDTO>();
            CreateMap<ParkingBookingDTO, ParkINBooking>();

            // User mappings
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }

    }
}
