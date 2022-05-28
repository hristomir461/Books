using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace BookAPI.Models
{
    public class BookContext : DbContext
    {
        public BookContext()
        {

        }
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
          
        }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Tvoqt ConnectionString");
            }
        }

    }
}
