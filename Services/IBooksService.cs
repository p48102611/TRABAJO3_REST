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
        public BookModel GetBook(long  bookId);
        public BookModel CreateBook(BookModel newBook);
        public bool DeleteTeam(long bookId);
        public BookModel UpdateBook(long bookId, BookModel updateBook);


    }
}
