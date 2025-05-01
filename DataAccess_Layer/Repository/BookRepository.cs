using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer.Repository
{
    public class BookRepository :IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<BookModel?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<BookModel> AddBookAsync(BookModel book)
        {
            book.CreatedAtDate = DateTime.UtcNow;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<BookModel?> UpdateBookAsync(int id, BookModel book)
        {
            var existing = await _context.Books.FindAsync(id);
            if (existing == null) return null;

            book.UpdatedAtDate = DateTime.UtcNow;
            _context.Entry(existing).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
