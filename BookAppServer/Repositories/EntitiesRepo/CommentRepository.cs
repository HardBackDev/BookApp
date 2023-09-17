using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BookAppServer.Repositories.EntitiesRepo
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Comment>> GetAll(CommentParameters parameters)
        {
            var comments = await FindAll()
                .Where(c => c.Text.Contains(parameters.SearchedText))
                .OrderBy(c => c.Text)
                .ToListAsync();

            return PagedList<Comment>.ToPagedList(comments, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Comment>> GetCommentsByBook(int bookId, CommentParameters parameters)
        {
            var comments = await
                FindAll()
                .Where(c => c.BookId.Equals(bookId)
                && c.Text.Contains(parameters.SearchedText))
                .Include(c => c.User)
                .ToListAsync();

            return PagedList<Comment>.ToPagedList(comments, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<Comment>> GetCommentsByUser(string userId, CommentParameters parameters)
        {
            var comments = await
                FindAll()
                .Where(c => c.UserId.Equals(userId)
                && c.Text.Contains(parameters.SearchedText))
                .Include(c => c.User)
                .ToListAsync();

            return PagedList<Comment>.ToPagedList(comments, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Comment> GetById(int id) => 
            await FindByCondition(c => c.Id.Equals(id))
            .FirstOrDefaultAsync();

        public void CreateComment(Comment comment) => Create(comment);

        public void DeleteComment(Comment comment) => Delete(comment);

        public async void UpdateComment(Comment comment) => await Update(comment);
    }
}
