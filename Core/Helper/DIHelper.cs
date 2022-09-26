using DAL.Helper;
using DAL.Logger;
using FileSystemLoader;
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
using Settings;
using Settings.Interface;

namespace Core.Helper
{
    public static class DIHelper
    {
        public static void RegisterType(this IServiceCollection builder)
        {
            builder.AddSingleton<ISettingLoader, SettingLoader>();
            builder.AddSingleton<AuthConfig>();
            builder.AddScoped<IUserService, UserService>();
            builder.AddSingleton<DBConfig>();
            builder.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.AddScoped<UserManagerLogger>();
            builder.AddScoped<IPathGenerator, PathGenerator>();
            builder.AddScoped<IPreviewGenerator, PreviewGenerator>();
            builder.AddScoped<IFileService>(p => new FileService(p.GetService<IStreamWorker>(), p.GetService<IUnitOfWork>(), FileMapper.FileConfig(), p.GetService<IPathGenerator>(), p.GetService<IPreviewGenerator>()));
            builder.AddTransient<IStreamWorker, StreamWorker>();
            builder.Decorate<IStreamWorker, CacheStreamWorker>();
        }
    }
}
