using DAL.Helper;
using DAL.Logger;
using FileSystemLoader.Interface;
using FileSystemLoader.Loaders;
using FileSystemLoader.MapperConfig;
using FileSystemLoader.Service;
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
            builder.AddScoped<IUserService, UserService>();
            builder.AddSingleton<DBConfig>();
            builder.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.AddScoped<UserManagerLogger>();
            builder.AddScoped<IFileService>(p => new FileService(p.GetService<IStreamWorker>(), p.GetService<IUnitOfWork>(), FileMapper.FileConfig()));
            builder.AddTransient<IStreamWorker, StreamWorker>();
            builder.Decorate<IStreamWorker, CacheStreamWorker>();
        }
    }
}
