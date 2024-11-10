using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Domain.Models;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Fine> Fines { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ApplicationUser to Borrowing - One to Many
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrowings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book to Borrowing - One to Many
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book)
                .WithMany(book => book.Borrowings)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Borrowing to Fine - One to Many
            modelBuilder.Entity<Fine>()
                .HasOne(f => f.Borrowing)
                .WithMany(b => b.Fines)
                .HasForeignKey(f => f.BorrowingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book to Inventory - One to One
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Book)
                .WithOne(b => b.Inventory)
                .HasForeignKey<Inventory>(i => i.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Genre to Book - One to Many
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Cascade);  

            // Unique constraint on ISBN
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();  


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Borrowings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Fines)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
