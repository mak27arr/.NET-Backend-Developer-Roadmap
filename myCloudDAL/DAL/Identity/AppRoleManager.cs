using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.DAL.Identity
{
    public class AppRoleManager : RoleManager<AppRole>
    {
        public AppRoleManager(RoleStore<AppRole> store) : base(store){ }
    }
}
