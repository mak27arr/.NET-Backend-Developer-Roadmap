using myCloudDAL.DAL.Identity;
using myCloudDAL.DAL.Interface.Repo;

namespace myCloudDAL.DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        AppUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        AppRoleManager RoleManager { get; }
        IFileRepository FileRepository { get; }
        IPreviewRepository PreviewRepository { get; }
        Task SaveAsync();
    }
}
