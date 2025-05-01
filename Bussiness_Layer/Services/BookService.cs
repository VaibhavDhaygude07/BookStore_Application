using Bussiness_Layer.Interfaces;
using DataAccess_Layer.Interfaces;
using DataAccess_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<BookModel>> GetAllBooksAsync() => _repository.GetAllBooksAsync();
        public Task<BookModel?> GetBookByIdAsync(int id) => _repository.GetBookByIdAsync(id);
        public Task<BookModel> AddBookAsync(BookModel book) => _repository.AddBookAsync(book);
        public Task<BookModel?> UpdateBookAsync(int id, BookModel book) => _repository.UpdateBookAsync(id, book);
        public Task<bool> DeleteBookAsync(int id) => _repository.DeleteBookAsync(id);
    }
}
