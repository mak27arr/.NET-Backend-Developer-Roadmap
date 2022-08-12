using Microsoft.AspNet.Identity.EntityFramework;

namespace myCloudDAL.DAL.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
