using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.File;
using myCloudDAL.EF.Config;

namespace myCloudDAL.DAL
{
    internal class AppDBContext : DbContext
    {
        internal DbSet<UserFile<Guid>> UserFiles;
        internal DbSet<PreviewFile<Guid>> PreviewFiles;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PreviewFileConfiguration());
            modelBuilder.ApplyConfiguration(new UserFileConfig());
        }
    }
}
