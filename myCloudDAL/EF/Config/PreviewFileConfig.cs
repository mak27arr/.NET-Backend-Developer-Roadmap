using Microsoft.EntityFrameworkCore;
using myCloudDAL.DAL.Entities.File;

namespace myCloudDAL.EF.Config
{
    internal class PreviewFileConfiguration : IEntityTypeConfiguration<PreviewFile<Guid>>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PreviewFile<Guid>> builder)
        {
            builder.ToTable(nameof(PreviewFile<Guid>));
            builder.HasKey(t => new { t.Id });
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasOne(u => u.File).WithOne(p => p.PreviewFile).HasForeignKey<PreviewFile<Guid>>(p => p.Id);
        }
    }
}
