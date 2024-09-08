using BookManagementApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApplication.DbAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<BookModel> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Books)
                .WithOne(u => u.Users)
                .HasForeignKey(u => u.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
