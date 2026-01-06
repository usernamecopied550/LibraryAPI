using Microsoft.EntityFrameworkCore;
using LibraryApi.Models;
using System.Collections.Generic;

namespace LibraryApi.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Copy> Copies => Set<Copy>();
    }
}
