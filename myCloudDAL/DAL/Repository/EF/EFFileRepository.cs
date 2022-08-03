using myCloudDAL.DAL.Entities.File;
using myCloudDAL.DAL.Interface.Repo;

namespace myCloudDAL.DAL.Repository.EF
{
    internal class EFFileRepository : BaseEFRepository<UserFile<Guid>, Guid>, IFileRepository
    {

        public EFFileRepository(AppDBContext context) : base(context, context.UserFiles)
        {

        }

        public override Task<UserFile<Guid>> GetAsync(Guid id) => Task.FromResult(_dbSet.FirstOrDefault(x => x.Id == id));
    }
}
