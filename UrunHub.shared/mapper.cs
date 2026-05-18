using AutoMapper;
using UrbanHub.DTO;
using UrbanHub.Entities;

namespace UrbanHub.shared
{
    public class mapper : Profile
    {
        public mapper()
        {
            CreateMap<ParkingSpace, ParkingSpaceDTO>();
            CreateMap<ParkingSpaceDTO, ParkingSpace>();
        }

    }
}
