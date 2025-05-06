using BookCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookCatalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; } = null!;
    }
    public class DataSeed
    {
        public static async Task SeedData(ApplicationDbContext db)
        {
            if (!db.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", PageCount = 180 },
                    new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", PageCount = 281 },
                    new Book { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Dystopian", PageCount = 328 }
                };
                db.Books.AddRange(books);
                await db.SaveChangesAsync();
            }
        }
    }
}