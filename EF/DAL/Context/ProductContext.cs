using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.DAL.Context
{
    internal class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions options)
        {
               Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(t => new { t.Id });
            modelBuilder.Entity<Product>().Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}
