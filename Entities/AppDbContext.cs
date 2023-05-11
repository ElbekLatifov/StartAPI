using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Api.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=data.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        //Fluent API configuration
        modelBuilder.Entity<Product>().HasOne(p=>p.Category).WithMany(p=>p.Products).HasForeignKey(p=>p.CategoryId);
        modelBuilder.Entity<Category>().HasOne(p=>p.Parent).WithMany(p=>p.Children).HasForeignKey(p=>p.ParentId).OnDelete(DeleteBehavior.NoAction);
    }
}