using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.DAL.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store, ILogger<UserManager<AppUser>> logger) : base(store, null, new PasswordHasher<AppUser>(), null, null, null, null, null, logger)
        {
        }
    }
}
