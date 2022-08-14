using API.ViewModel.Auth;
using AutoMapper;
using DTO.Identity;

namespace API.Helper.Mapper
{
    public static class MapConfig
    {
        public static MapperConfiguration AuthConfig()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterVM, UserDTO>();
                cfg.CreateMap<LoginVM, UserDTO>();
                });
        }
    }
}
