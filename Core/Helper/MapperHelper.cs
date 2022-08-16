using Core.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Helper
{
    public static class MapperHelper
    {
        public static void AddAllMappers(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AppUserMappingProfile));
        }
    }
}
