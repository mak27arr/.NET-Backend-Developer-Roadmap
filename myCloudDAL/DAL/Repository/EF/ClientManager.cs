using myCloudDAL.DAL.Entities.Identity;
using myCloudDAL.EF;

namespace myCloudDAL.DAL.Interface.Repo
{
    internal class ClientManager : IClientManager
    {
        private AppDBContext _database;

        public ClientManager(AppDBContext db)
        {
            _database = db;
        }

        public void Create(ClientProfile item)
        {
            _database.ClientProfiles.Add(item);
            _database.SaveChanges();
        }

        public void Dispose() => _database.Dispose();
    }
}
