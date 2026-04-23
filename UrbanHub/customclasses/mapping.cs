using AutoMapper;
using UrbanHub.Data.Entities;
using UrbanHub.DTO;

namespace UrbanHub.customclasses
{
    public class mapping:Profile
    {
        public mapping()
        {
            CreateMap<Registration, RegistrationDTO>();
            CreateMap<RegistrationDTO, Registration>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
