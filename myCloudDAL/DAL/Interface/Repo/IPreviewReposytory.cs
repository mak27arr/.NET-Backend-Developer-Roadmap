using myCloudDAL.DAL.Entities.File;
using myCloudDAL.DAL.Interface.Repo;

namespace myCloudDAL.DAL.Interface.Repo
{
    internal interface IPreviewRepository : IRepository<PreviewFile<Guid>, Guid>
    {
    }
}
