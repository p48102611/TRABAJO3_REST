using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRABAJO3_REST.Exceptions;
using TRABAJO3_REST.Models;

namespace TRABAJO3_REST.Services
{
    public class BooksService : IBooksService
    {
        private IList<BookModel> _books;
        private HashSet<string> _allowdOrderByValues = new HashSet<string>()
        {
            "id", "title","author","subject"
        };
        public BooksService()
        {
            _books = new List<BookModel>();
            _books.Add(new BookModel()
            {
                Id = 1,
                Author = "Miguel Cervantes",
                Editorial = "Gisbert",
                Format = "pdf",
                Subject = "Drama, Romantico",
                Title = "Don Quijote de la Mancha"
            });
            _books.Add(new BookModel()
            {
                Id = 2,
                Author = "Dante Alighieri",
                Editorial = "Salvador",
                Format = "pdf",
                Subject = "Drama, Psicologico",
                Title = "Divina Comedia"
            });
            _books.Add(new BookModel()
            {
                Id = 3,
                Author = "Carmen Clara Balmaseda",
                Format = "pdf",
                Subject = "Novela Negra",
                Title = "La crisalida"
            });
            _books.Add(new BookModel()
            {
                Id = 4,
                Author = "Richard A. Knaak",
                Editorial = "Inedito",
                Format = "pdf",
                Subject = "Novela",
                Title = "Warcraft El pozo de la eternidad"
            });
        }
        public BookModel CreateBook(BookModel newBook)
        {
            var nextId = _books.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            newBook.Id = nextId;
            _books.Add(newBook);
            return newBook;
        }

        public bool DeleteTeam(long bookId)
        {
            var bookToDelete = GetBook(bookId);
            _books.Remove(bookToDelete);
            return true;
        }

        public BookModel GetBook(long bookId)
        {
            var book = _books.FirstOrDefault(t => t.Id == bookId);
            if (book == null)
            {
                throw new NotFoundItemException($"The team with id: {bookId} does not exists.");
            }
            return book;
        }

        public IEnumerable<BookModel> GetBooks(string orderBy = "Id")
        {
            if (!_allowdOrderByValues.Contains(orderBy.ToLower()))
                //esta parte de join es para aplanar una lista
                throw new InvalidOperationItemException($"The Orderbt value: {orderBy} is invalid, please use one of {String.Join(',', _allowdOrderByValues.ToArray()) }");
            switch (orderBy.ToLower())
            {
                case "title":
                    return _books.OrderBy(t => t.Title);
                case "author":
                    return _books.OrderBy(t => t.Author);
                case "subject":
                    return _books.OrderBy(t => t.Subject);
                default:
                    return _books.OrderBy(t => t.Id);
            }
        }

        public BookModel UpdateBook(long bookId, BookModel updateBook)
        {
            updateBook.Id = bookId;
            var book = GetBook(bookId);
            book.Title = updateBook.Title ?? book.Title;
            book.Author = updateBook.Author ?? book.Author;
            book.Editorial = updateBook.Editorial ?? book.Editorial;
            book.Format = updateBook.Format ?? book.Format;
            book.Like = updateBook.Like ?? book.Like;
            book.Subject = updateBook.Subject ?? book.Subject;
            return book;
        }
    }
}
