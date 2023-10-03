using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Extensions;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAppServer.Controllers
{
    [ApiController]
    [Route("api/userbooks")]
    public class UserBookController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UserBookController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("{bookId:int}")]
        [Authorize]
        public async Task<IActionResult> AddToFavoriteBooks(int bookId)
        {
            await _services.UserBookService.AddToFavoriteBooks(bookId, User.Identity.Name);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserBooks([FromQuery] BookParameters parameters)
        {
            var pagedResult = await _services.UserBookService.GetUserBooks(User.Identity.Name, parameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.books);
        }

        [Authorize]
        [HttpGet("{bookId:int}")]
        public async Task<IActionResult> CheckBookIsFavorite(int bookId)
        {
            var result = await _services.UserBookService.CheckBookInFavorites(User.Identity.Name, bookId);
            return Ok(result);
        }
    }
}
