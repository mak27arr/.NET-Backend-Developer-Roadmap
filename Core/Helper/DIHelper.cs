using DAL.Helper;
using Identity.Helper;
using Identity.Interfaces;
using Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using myCloudDAL.DAL.Interface;
using myCloudDAL.DAL.Repository.EF;

namespace Core.Helper
{
    public static class DIHelper
    {
        public static void RegisterType(this IServiceCollection builder)
        {
            builder.AddSingleton<AuthConfig>();
            builder.AddTransient<IUserService, UserService>();
            builder.AddSingleton<DBConfig>();
            builder.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
