using Microsoft.EntityFrameworkCore;
using HackerRank1.Entities.Models;

namespace HackerRank1.DataAccess.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        { }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
