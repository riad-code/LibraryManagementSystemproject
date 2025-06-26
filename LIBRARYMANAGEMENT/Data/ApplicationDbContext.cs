using LIBRARYMANAGEMENT.Models; // Make sure this is added
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

        // ✅ Register your models here
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }


        // You can add more entities below when needed
        // public DbSet<BorrowRecord> BorrowRecords { get; set; }
        // public DbSet<BookRequest> BookRequests { get; set; }
    }
}
