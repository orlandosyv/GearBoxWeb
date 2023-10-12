using GearBoxWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GearBoxWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1, Description = "Action and fights" },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2, Description = "Science Fiction" },
                new Category { Id = 3, Name = "History", DisplayOrder = 3, Description = "Historical" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
