using AutoMapper;
using OniHealth.Domain.Models;
using OniHealth.Domain.DTOs;

namespace OniHealth.Web.Config
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingsConfigs = new MapperConfiguration(config =>
            {
                config.CreateMap<Roles, RolesDTO>().ReverseMap();
                config.CreateMap<Customer, CustomerDTO>().ReverseMap();
                config.CreateMap<Employer, EmployerDTO>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap();
            });

            return mappingsConfigs;
        }
    }
}
