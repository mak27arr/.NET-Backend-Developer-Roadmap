using myCloudDAL.DAL.Entities.File;

namespace myCloudDAL.DAL.Interface.Repo
{
    internal interface IFileRepository : IRepository<UserFile<Guid>, Guid>
    {
    }
}
