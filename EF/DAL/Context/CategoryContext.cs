using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.DAL.Context
{
    internal class CategoryContext : DbContext
    {
        public DbSet<Category> Categorys { get; set; }

        public CategoryContext(DbContextOptions options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Product>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasIndex(x => x.Barcode).IsUnique();
        }
    }
}
