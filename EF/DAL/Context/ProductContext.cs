using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.DAL.Context
{
    internal class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions options) : base(options)
        {
               Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Product>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasOne(c => c.Category).WithMany(x => x.Products)
                .HasForeignKey(f => f.CategoryInfoKey)
                .HasForeignKey( k => k.CategoryName).HasPrincipalKey(p => p.Name);
        }
    }
}
