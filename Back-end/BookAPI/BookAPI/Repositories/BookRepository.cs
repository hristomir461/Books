using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }
        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Get()
        {
            var books = _context.Books.ToList();
            var bookTitles = _context.Books.Select(x => x.Title).ToList();
            if (bookTitles.Contains("Да убиеш присмехулник"))
            {
                var item = books.FirstOrDefault(x => x.Title == "Да убиеш присмехулник");
                books.Remove(item);
                books.Insert(0, item);
            }
            return books;
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
