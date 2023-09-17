using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.RepositoriesContracts
{
    public interface ICommentRepository
    {
        Task<PagedList<Comment>> GetAll(CommentParameters parameters);
        Task<Comment> GetById(int id);
        void CreateComment(Comment comment);
        void DeleteComment(Comment comment);
        void UpdateComment(Comment comment);
        Task<PagedList<Comment>> GetCommentsByBook(int bookId, CommentParameters parameters);
        Task<PagedList<Comment>> GetCommentsByUser(string userId, CommentParameters parameters);
    }
}
