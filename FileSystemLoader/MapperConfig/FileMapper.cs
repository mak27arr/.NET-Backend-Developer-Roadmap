using AutoMapper;
using DTO.File;
using myCloudDAL.DAL.Entities.File;

namespace FileSystemLoader.MapperConfig
{
    public static class FileMapper
    {
            public static IConfigurationProvider FileConfig()
            {
                return new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserFile<Guid>, UserFileDTO>().ReverseMap();
                    cfg.CreateMap<PreviewFile<Guid>, PreviewFileDTO>().ReverseMap();
                });
            }
    }
}
