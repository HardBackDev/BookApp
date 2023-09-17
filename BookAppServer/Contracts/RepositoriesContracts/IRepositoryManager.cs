using BookAppServer.Repositories.EntitiesRepo;

namespace BookAppServer.Contracts.RepositoriesContracts
{
    public interface IRepositoryManager
    {
        IBookRepository BookRepo { get; }
        IAuthorRepository AuthorRepo { get; }
        IUserBookRepository UserBookRepo { get; }
        ICommentRepository CommentRepo { get; }
        Task SaveAsync();
    }
}
