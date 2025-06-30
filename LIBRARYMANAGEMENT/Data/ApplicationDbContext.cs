using LIBRARYMANAGEMENT.Models; // Model references
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LIBRARYMANAGEMENT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ Register your models
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Optional future models
        // public DbSet<BorrowRecord> BorrowRecords { get; set; }
        // public DbSet<BookRequest> BookRequests { get; set; }

        // ✅ Seeding default categories
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Computer Science", Description = "Tech Books" },
                new Category { CategoryID = 2, CategoryName = "Literature", Description = "Novels" },
                new Category { CategoryID = 3, CategoryName = "Science", Description = "Science Books" }
            );
        }
    }
}