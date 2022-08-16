using AutoMapper;
using DTO.Identity;
using myCloudDAL.DAL.Entities.Identity;

namespace Core.Mapper
{
	public class AppUserMappingProfile : Profile
	{
		public AppUserMappingProfile()
		{
			CreateMap<AppUser, UserDTO>().ReverseMap();
		}
	}
}
