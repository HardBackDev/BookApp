using BookAppServer.Services;

namespace BookAppServer.Contracts.ServicesContracts
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
        IAuthorService AuthorService { get; }
        IUserBookService UserBookService { get; }
        ICommentService CommentService { get; }
        AuthenticationService UserService { get; }
    }
}
