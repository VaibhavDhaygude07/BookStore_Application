﻿using DataAccess_Layer.Models;

namespace DataAccess_Layer.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetAllBooksAsync();
        Task<BookModel?> GetBookByIdAsync(int id);
        Task<BookModel> AddBookAsync(BookModel book);
        Task<BookModel?> UpdateBookAsync(int id, BookModel book);
        Task<bool> DeleteBookAsync(int id);


        Task<IEnumerable<BookModel>> SearchBooksAsync(string searchText);

        Task<IEnumerable<BookModel>> SortBooksByPriceAsync(string price);   

        List<BookModel> GetBooksByPageNumber(int pageNumber);


    }
}
