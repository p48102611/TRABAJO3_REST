using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRABAJO3_REST.Exceptions;
using TRABAJO3_REST.Models;
using TRABAJO3_REST.Services;

namespace TRABAJO3_REST.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookModel>> GetBooks(string by = "", string nameBy = "", string orderBy = "")
        {
            try
            {
                if (orderBy == "" && nameBy == "" && by == "")
                    return Ok(_booksService.GetBooks("Id"));
                if (orderBy == "")
                {
                    var books = _booksService.SearchBooksBy(by, nameBy);
                    return Ok(books);
                }
                else
                    return Ok(_booksService.GetBooks(orderBy));
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something unexpected happened.");
            }
        }
        [HttpGet("search")]
        public ActionResult<IEnumerable<BookModel>> SearchBooksBy(string by = "", string nameBy = "")
        {
            try
            {
                var books = _booksService.SearchBooksBy(by, nameBy);
                return Ok(books);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something unexpected happened.");
            }
        }

        [HttpGet("{bookId:long}")]
        //public ActionResult<TeamModel> GetTeam(long teamId, string algo) // si tiene un parametro mas asume que es un query param
        public ActionResult<BookModel> GetBook(long bookId)
        {
            try
            {
                var book = _booksService.GetBook(bookId);
                return Ok(book);
            }
            catch (NotFoundItemException ex)
            {
                //pasamos el mensaje de la excepcion creada
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPost]
        public ActionResult<BookModel> CreateTeam([FromBody] BookModel newBook)
        {
            //para crear
            try
            {
                //el status code se devolvera depende lo que requiera el modelo
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var book = _booksService.CreateBook(newBook);
                return Created($"api/books/{book.Id}", newBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [HttpDelete]
        [HttpDelete("{bookId:long}")]
        public ActionResult<bool> DeleteBook(long? bookId, string by, string nameBy)
        {
            try
            {
                if(bookId != null)
                {
                    var result = _booksService.DeleteBook(bookId);
                    return Ok(result);
                }
                else
                {
                    _booksService.DeleteBooksBy(by, nameBy);
                    return Ok();
                }
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("delete")]
        public ActionResult<bool> DeleteBooksBy(string by, string nameBy)
        {
            try
            {
                _booksService.DeleteBooksBy(by, nameBy);
                return Ok();
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{bookId:long}")]
        public ActionResult<BookModel> UpdateTeam(long bookId, [FromBody] BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                var natinallity = ModelState[nameof(bookModel.CountLike)];

                if (natinallity != null && natinallity.Errors.Any())
                {
                    return BadRequest(natinallity.Errors);
                }
            }
            try
            {
                var book = _booksService.UpdateBook(bookId, bookModel);
                return Ok(book);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
    }
}
