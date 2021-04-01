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
            "id", "title","author","subject, pages"
        };
        private HashSet<string> _allowdOrderByValuesSearch = new HashSet<string>()
        {
            "author","subject","editorial","format"
        };
        private HashSet<string> _allowdOrderByValuesDelete = new HashSet<string>()
        {
            "title, author","subject","editorial","format"
        };
        public BooksService()
        {
            _books = new List<BookModel>();
            _books.Add(new BookModel()
            {
                Id = 1,
                Author = "MiguelCervantes",
                Editorial = "Gisbert",
                Format = "pdf",
                Subject = "Aventura",
                Title = "Don Quijote de la Mancha"
            });
            _books.Add(new BookModel()
            {
                Id = 2,
                Author = "DanteAlighieri",
                Editorial = "Salvador",
                Format = "pdf",
                Subject = "Drama, Psicologico",
                Title = "Divina Comedia"
            });
            _books.Add(new BookModel()
            {
                Id = 3,
                Author = "Carmea",
                Format = "pdf",
                Subject = "Novela",
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
            _books.Add(new BookModel()
            {
                Id = 5,
                Author = "MiguelCervantes",
                Editorial = "Gisbert",
                Format = "pdf",
                Subject = "Psicologico",
                Title = "El trato del angel"
            });
            _books.Add(new BookModel()
            {
                Id = 6,
                Author = "MiguelCervantes",
                Editorial = "Gisbert",
                Format = "pdf",
                Subject = "Drama, Romantico",
                Title = "El cerco de numancia"
            });
        }
        public BookModel CreateBook(BookModel newBook)
        {
            var nextId = _books.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            newBook.Id = nextId;
            _books.Add(newBook);
            return newBook;
        }

        public bool DeleteBook(long? bookId)
        {
            var bookToDelete = GetBook(bookId);
            _books.Remove(bookToDelete);
            return true;
        }

        public void DeleteBooksBy(string by, string nameBy)
        {
            if (by != "" && nameBy == null)
                throw new InvalidOperationItemException($"The (nameby) field is empty, this field is required");
            if (by == null && nameBy != "")
                throw new InvalidOperationItemException($"The (by) field is empty, this field is required");
            if (!_allowdOrderByValues.Contains(by.ToLower()))
                throw new InvalidOperationItemException($"The find value: {by} is invalid, please use one of {String.Join(',', _allowdOrderByValuesDelete.ToArray()) }");

            switch (by.ToLower())
            {
                case "title":
                    DeleteBooksComplemet(_books.Where(t => t.Title.ToLower() == nameBy).ToList());
                    break;
                case "author":
                    DeleteBooksComplemet(_books.Where(t => t.Author.ToLower() == nameBy).ToList());
                    break;
                case "subject":
                    DeleteBooksComplemet(_books.Where(t => t.Subject.ToLower() == nameBy).ToList());
                    break;
                case "editorial":
                    DeleteBooksComplemet(_books.Where(t => t.Subject.ToLower() == nameBy).ToList());
                    break;
                case "format":
                    DeleteBooksComplemet(_books.Where(t => t.Subject.ToLower() == nameBy).ToList());
                    break;
                default:
                    break;
            }
        }
        public void DeleteBooksComplemet(IList<BookModel> books)
        {
            foreach (var item in books)
            {
                DeleteBook(item.Id);
            }
        }
        public BookModel GetBook(long? bookId)
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
                throw new InvalidOperationItemException($"The Orderbt value: {orderBy} is invalid, please use one of {String.Join(',', _allowdOrderByValues.ToArray()) }");
            switch (orderBy.ToLower())
            {
                case "title":
                    return _books.OrderBy(t => t.Title);
                case "author":
                    return _books.OrderBy(t => t.Author);
                case "subject":
                    return _books.OrderBy(t => t.Subject);
                case "pages":
                    return _books.OrderBy(t => t.Pages);
                default:
                    return _books.OrderBy(t => t.Id);
            }
        }
        public IEnumerable<BookModel> SearchBooksBy(string by, string nameBy)
        {
            var books = SearchComplemet(by, nameBy);
            if (books.Count() == 0)
                throw new NotFoundItemException($"The name: {nameBy} does not exists.");
            return books;
        }
        public IEnumerable<BookModel> SearchComplemet(string by, string nameBy)
        {
            if (by != "" && nameBy == null)
                throw new InvalidOperationItemException($"The (nameFind) field is empty, this field is required");
            if (by == null && nameBy != "")
                throw new InvalidOperationItemException($"The (find) field is empty, this field is required");
            if (!_allowdOrderByValues.Contains(by.ToLower()))
                throw new InvalidOperationItemException($"The find value: {by} is invalid, please use one of {String.Join(',', _allowdOrderByValuesSearch.ToArray()) }");
            switch (by.ToLower())
            {
                case "author":
                    return _books.Where(t => t.Author.ToLower() == nameBy).ToList();
                case "subject":
                    return _books.Where(t => t.Subject.ToLower() == nameBy).ToList();
                case "editorial":
                    return _books.Where(t => t.Editorial.ToLower() == nameBy).ToList();
                case "format":
                    return _books.Where(t => t.Format.ToLower() == nameBy).ToList();
                default:
                    return null;
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
            book.CountLike = updateBook.CountLike ?? book.CountLike;
            book.Subject = updateBook.Subject ?? book.Subject;
            return book;
        }
    }
}
