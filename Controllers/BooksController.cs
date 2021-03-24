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
        public ActionResult<IEnumerable<BookModel>> GetBooks(string orderBy = "Id")
        {
            try
            {
                var books = _booksService.GetBooks(orderBy);
                return Ok(books);
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
        //api/teams/2
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
                return Created($"api/teams/{book.Id}", newBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{bookId:long}")]
        public ActionResult<bool> DeleteBook(long bookId)
        {
            try
            {
                var result = _booksService.DeleteTeam(bookId);
                return Ok(result);
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

        [HttpPut("{teamId:long}")]
        public ActionResult<BookModel> UpdateTeam(long bookId, [FromBody] BookModel bookTeam)
        {
            try
            {
                var book = _booksService.UpdateBook(bookId, bookTeam);
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
    }
}
