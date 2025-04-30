using BookCatalog.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Data
{
    public class DataSeed
    {
        public static async Task SeedData(DbContext db)
        {
            List<Book> books = new()
            {
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Fiction",
                    PageCount = 180
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    PageCount = 281
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Dystopian",
                    PageCount = 328
                },
            };
            await db.Set<Book>().AddRangeAsync(books);
            await db.SaveChangesAsync();
        }
    }
}
