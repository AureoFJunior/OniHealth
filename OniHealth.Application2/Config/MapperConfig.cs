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
                config.CreateMap<Plans, PlansDTO>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap();
                config.CreateMap<Consult, ConsultDTO>().ReverseMap();
                config.CreateMap<ConsultType, ConsultTypeDTO>().ReverseMap();
            });

            return mappingsConfigs;
        }
    }
}
