using Azure.Core;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.CommentDto;
using BookAppServer.Exceptions;
using BookAppServer.Extensions;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BookAppServer.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CommentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] CommentParameters parameters)
        {
            var pagedResult = await _service.CommentService.GetComments(parameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.comments);
        }

        [HttpGet("byBook/{bookId:int}")]
        public async Task<IActionResult> GetCommentsByBook(int bookId, [FromQuery] CommentParameters parameters)
        {
            var pagedResult = await _service.CommentService.GetCommentsByBook(bookId, parameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.comments);
        }

        [Authorize]
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetCommentsByUser([FromQuery] CommentParameters parameters)
        {
            var pagedResult = await _service.CommentService.GetCommentsByUser(User.Identity.Name, parameters);
            Response.AddPagedMetadataHeaders(pagedResult.metaData);

            return Ok(pagedResult.comments);
        }

        [Authorize]
        [HttpPost("{bookId:int}")]
        public async Task<IActionResult> AddComment(int bookId, [FromBody] CommentForCreation comment)
        {
            var userName = User.Identity.Name;
            await _service.CommentService.AddComment(bookId, userName, comment);
            return Ok();
        }

        [Authorize]
        [HttpPut("{commentId:int}")]
        public async Task<IActionResult> EditComment(int commentId, [FromBody] CommentForUpdate comment)
        {
            CheckIsUserComment(comment.UserName, User);
            await _service.CommentService.EditComment(commentId, comment);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:int}/{userName}")]
        public async Task<IActionResult> DeleteComment(int id, string userName)
        {
            CheckIsUserComment(userName, User);
            await _service.CommentService.DeleteComment(id);
            return Ok();
        }

        void CheckIsUserComment(string userName, ClaimsPrincipal user)
        {
            var userRole = user.Identities.First().Claims.First(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

            if (!userName.Equals(user.Identity.Name) && userRole != "Admin")
            {
                throw new BadRequestException($"The non-admin user, {User.Identity.Name}, cannot edit or delete comments belonging to {userName}");
            }
        }
    }
}
