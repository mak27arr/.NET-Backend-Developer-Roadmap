using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.Identity;

namespace myCloudDAL.EF
{
    internal class IdentityAppContext : IdentityDbContext<AppUser>
    {
        public IdentityAppContext(string contextString) : base(contextString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
