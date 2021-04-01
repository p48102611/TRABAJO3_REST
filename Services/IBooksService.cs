using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRABAJO3_REST.Models;

namespace TRABAJO3_REST.Services
{
    public interface IBooksService
    {
        public IEnumerable<BookModel> GetBooks(string orderBy = "Id");
        public IEnumerable<BookModel> SearchBooksBy(string by, string nameBy);
        public BookModel GetBook(long? bookId);
        public BookModel CreateBook(BookModel newBook);
        public bool DeleteBook(long? bookId);
        public void DeleteBooksBy(string field, string nameField);
        public BookModel UpdateBook(long bookId, BookModel updateBook);


    }
}
