using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.DAL.Interface
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
