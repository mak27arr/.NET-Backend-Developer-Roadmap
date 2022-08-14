using Microsoft.AspNetCore.Identity;
using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.DAL.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store, null, new PasswordHasher<AppUser>(), null, null, null, null, null, null)
        {
        }
    }
}
