using BookAppServer.Dto.CommentDto;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace BookAppServer.Contracts.ServicesContracts
{
    public interface ICommentService
    {
        Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsByBook(int bookId, CommentParameters parameters);
        Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetCommentsByUser(string userId, CommentParameters parameters);
        Task<(IEnumerable<CommentDto> comments, MetaData metaData)> GetComments(CommentParameters parameters);
        Task AddComment(int bookId, string userName, CommentForCreation comment);
        Task EditComment(int id, CommentForUpdate comment);
        Task DeleteComment(int id);
    }
}
