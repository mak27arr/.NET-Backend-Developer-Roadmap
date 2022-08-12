using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.Identity;
using myCloudDAL.DAL.Identity;
using myCloudDAL.DAL.Interface;
using myCloudDAL.DAL.Interface.Repo;
using myCloudDAL.EF;

namespace myCloudDAL.DAL.Repository.EF
{
    internal class UnitOfWork : IUnitOfWork
    {
        private IdentityAppContext _identityDb;
        private AppDBContext _dataDb;

        #region Repository
        public AppUserManager UserManager { get; }
        public IClientManager ClientManager { get; }
        public AppRoleManager RoleManager { get; }
        public IFileRepository FileRepository { get; }
        public IPreviewRepository PreviewRepository { get; }
        #endregion

        public UnitOfWork(string connectionString)
        {
            _identityDb = new IdentityAppContext(connectionString);
            UserManager = new AppUserManager(new UserStore<AppUser>(_identityDb));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(_identityDb));
            ClientManager = new ClientManager(_identityDb);

            #if DEBUG
            var dbOption = new DbContextOptionsBuilder()
                            .UseSqlServer(connectionString)
                            .Options;
            #else
            var dbOption = new DbContextOptionsBuilder()
                            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                            .UseMySqlLolita()
                            .Options;
            #endif
            _dataDb = new AppDBContext(dbOption);
            FileRepository = new EFFileRepository(_dataDb);
            PreviewRepository = new EFPreviewRepository(_dataDb);
        }

        public async Task SaveAsync()
        {
            await _dataDb.SaveChangesAsync();
            await _identityDb.SaveChangesAsync();
        }

#region IDispose

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager?.Dispose();
                    ClientManager?.Dispose();
                    RoleManager?.Dispose();
                    FileRepository?.Dispose();
                    PreviewRepository?.Dispose();
                }
                this.disposed = true;
            }
        }

#endregion
    }
}
