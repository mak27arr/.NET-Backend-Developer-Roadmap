using API.ViewModel.Auth;
using API.ViewModel.File;
using AutoMapper;
using DTO.File;
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

        internal static AutoMapper.IConfigurationProvider FileConfig()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<FileVM, UserFileDTO>().ReverseMap();
            });
        }
    }
}
