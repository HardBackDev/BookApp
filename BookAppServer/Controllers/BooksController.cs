using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Extensions;
using BookAppServer.Filters;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BookAppServer.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _service;
        public BooksController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> GetBooks([FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _service.BookService.GetAllBooks(bookParameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.books);
        }

        [HttpGet("{id:int}", Name = "BookById")]
        public async Task<IActionResult> GetBook(int id)
        {
            Thread.Sleep(3000);

            var book = await _service.BookService.GetBook(id);
            return Ok(book);
        }

        [HttpGet("ByAuthor/{id:int}")]
        public async Task<IActionResult> GetBooksByAuthor(int id, [FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _service.BookService.GetBooksByAuthor(id, bookParameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.books);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> AddBook([FromBody] BookForCreation bookForCreation)
        {
            var book = await _service.BookService.CreateBook(bookForCreation);
            return CreatedAtRoute("BookById", new { id = book.Id }, book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateBooks")]
        [ValidationFilter]
        public async Task<IActionResult> AddBooks([FromBody] IEnumerable<BookForCreation> booksForCreation)
        {
            var booksForReturn = await _service.BookService.CreateBooks(booksForCreation);
            return Ok(booksForReturn);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateBook( int id, [FromBody] BookForUpdate bookForCreation)
        {
            await _service.BookService.UpdateBook(id, bookForCreation);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook( int id)
        {
            await _service.BookService.DeleteBook(id);
            return Ok();
        }
    }
}
