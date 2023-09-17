using AutoMapper;
using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Models;
using Microsoft.AspNetCore.Identity;

namespace BookAppServer.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<IUserBookService> _userBookService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<AuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager,
            IConfiguration configuration)
        {
            _bookService = new Lazy<IBookService>(() =>
                new BookService(repositoryManager, mapper));
            _authorService = new Lazy<IAuthorService>(() =>
                new AuthorService(repositoryManager, mapper));
            _authenticationService = new Lazy<AuthenticationService>(() =>
                new AuthenticationService(mapper, userManager, configuration));
            _userBookService = new Lazy<IUserBookService>(() =>
                new UserBookService(mapper, repositoryManager, userManager));
            _commentService = new Lazy<ICommentService>(() =>
                new CommentService(repositoryManager, userManager, mapper));
        }
        public IBookService BookService => _bookService.Value;
        public IAuthorService AuthorService => _authorService.Value;
        public IUserBookService UserBookService => _userBookService.Value;
        public ICommentService CommentService => _commentService.Value;
        public AuthenticationService UserService => _authenticationService.Value;
    }
}
