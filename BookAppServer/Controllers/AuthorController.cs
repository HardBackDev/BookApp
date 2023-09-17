using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.AuthorsDto;
using BookAppServer.Dto.BooksDto;
using BookAppServer.Extensions;
using BookAppServer.Filters;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookAppServer.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthorController(IServiceManager service) => _service = service;
        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorParameters authorParameters)
        {
            var pagedResult = await _service.AuthorService.GetAllAuthors(authorParameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.authors);
        }
        [HttpGet("{id:int}", Name = "AuthorById")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var authordDetails = await _service.AuthorService.GetAuthor(id);
            return Ok(authordDetails);
        }

        [HttpGet("{id:int}/books")]
        public async Task<IActionResult> GetBooksByAuthor(int id, [FromQuery] BookParameters bookParameters)
        {
            var pagedResult = await _service.AuthorService.GetBooksByAuthor(id, bookParameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.books);
        }

        [Authorize(Roles = "Admin")]
        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorForCreation authorForCreation)
        {
            var author = await _service.AuthorService.CreateAuthor(authorForCreation);
            return CreatedAtRoute("AuthorById", new {id = author.Id}, author);
        }

        [Authorize(Roles = "Admin")]
        [ValidationFilter]
        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddBooksForAuthor(int id,[FromBody] IEnumerable<BookForAuthorCreation> books)
        {
            await _service.AuthorService.CreateBooksForAuthor(id, books);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [ValidationFilter]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorForUpdate authorForUpdate)
        {
            await _service.AuthorService.UpdateAuthor(id, authorForUpdate);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            Console.WriteLine(id);
            await _service.AuthorService.DeleteAuthor(id);
            return Ok();
        }
    }
}
