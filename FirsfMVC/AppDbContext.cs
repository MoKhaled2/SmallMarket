using FirsfMVC.Models;
using Microsoft.EntityFrameworkCore;
using FirsfMVC.Areas.Employee.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FirsfMVC
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasData(
                new Category { id = 1, Name = "Select Category" },
                new Category { id = 2, Name = "Fruits" },
                new Category { id = 3, Name = "vegetables" },
                new Category { id = 4, Name = "Other" }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            );
        }
        public DbSet<EmployeesViewModel> Employee { get; set; } = default!;




    }
}
