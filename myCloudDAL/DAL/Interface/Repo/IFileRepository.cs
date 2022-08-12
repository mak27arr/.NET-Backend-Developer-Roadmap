using myCloudDAL.DAL.Entities.File;

namespace myCloudDAL.DAL.Interface.Repo
{
    public interface IFileRepository : IRepository<UserFile<Guid>, Guid>, IDisposable
    {
    }
}
