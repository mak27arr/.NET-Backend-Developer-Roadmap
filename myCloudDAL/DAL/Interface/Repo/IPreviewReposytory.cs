using myCloudDAL.DAL.Entities.File;
using myCloudDAL.DAL.Interface.Repo;

namespace myCloudDAL.DAL.Interface.Repo
{
    public interface IPreviewRepository : IRepository<PreviewFile<Guid>, Guid>, IDisposable
    {
    }
}
