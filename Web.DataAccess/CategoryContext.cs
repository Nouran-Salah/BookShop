using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models.Models;
namespace Web.DataAccess
{
    public class CategoryContext : DbContext
    {
    
        public CategoryContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   modelBuilder.Entity<Category>()
                .ToTable("Categories", "MasterSchema");
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.catName)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(c => c.catOrder)
                .IsRequired();
            modelBuilder.Entity<Category>()
                .Property(c => c.markedAsDeleted)
                .HasColumnName("is deleted")
                .HasDefaultValue(false);
            modelBuilder.Entity<Category>()
                .Ignore(c => c.createdDate);


            modelBuilder.Entity<Product>()
               .ToTable("Product", "MasterSchema");

            modelBuilder.Entity<Product>()
                .HasKey(p=>p.Id);
            modelBuilder.Entity<Product>()
                .Property(p => p.Title).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p=>p.Description).HasMaxLength(250);
            modelBuilder.Entity<Product>()
                .Property(p=>p.Author).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.price).IsRequired().HasColumnName("ProductPrice");
        }
    }
}
