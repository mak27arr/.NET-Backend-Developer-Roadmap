using DAL.Helper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.Identity;
using myCloudDAL.DAL.Identity;
using myCloudDAL.DAL.Interface;
using myCloudDAL.DAL.Interface.Repo;

namespace myCloudDAL.DAL.Repository.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext _dataDb;

        #region Repository
        public AppUserManager UserManager { get; }
        public IClientManager ClientManager { get; }
        public AppRoleManager RoleManager { get; }
        public IFileRepository FileRepository { get; }
        public IPreviewRepository PreviewRepository { get; }
        #endregion

        public UnitOfWork(DBConfig config)
        {
            #if DEBUG
            var dbOption = new DbContextOptionsBuilder()
                            .UseSqlite(config.ConnectionString)
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
            UserManager = new AppUserManager(new UserStore<AppUser>(_dataDb));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(_dataDb));
            ClientManager = new ClientManager(_dataDb);
        }

        public async Task SaveAsync()
        {
            await _dataDb.SaveChangesAsync();
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
