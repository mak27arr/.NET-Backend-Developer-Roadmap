using Microsoft.AspNet.Identity;
using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.DAL.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {
        }
    }
}
