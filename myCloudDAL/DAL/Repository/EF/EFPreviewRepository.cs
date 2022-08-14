using myCloudDAL.DAL.Entities.File;
using myCloudDAL.DAL.Interface.Repo;
using myCloudDAL.EF;

namespace myCloudDAL.DAL.Repository.EF
{
    internal class EFPreviewRepository : BaseEFRepository<PreviewFile<Guid>, Guid>, IPreviewRepository
    {

        public EFPreviewRepository(AppDBContext context) : base(context, context.PreviewFiles)
        {

        }

        public override Task<PreviewFile<Guid>> GetAsync(Guid id) => Task.FromResult(_dbSet.FirstOrDefault(x => x.Id == id));
    }
}
