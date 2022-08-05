using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.File;

namespace myCloudDAL.EF.Config
{
    internal class UserFileConfig : IEntityTypeConfiguration<UserFile<Guid>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserFile<Guid>> builder)
        {
            builder.ToTable(nameof(UserFile<Guid>));
            builder.HasKey(t => new { t.Id });
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne(u => u.PreviewFile).WithOne(p => p.File).HasForeignKey<PreviewFile<Guid>>(p => p.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
