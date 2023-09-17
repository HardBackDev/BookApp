using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Repositories.EntitiesRepo;

namespace BookAppServer.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IBookRepository> _bookRepository;
        private readonly Lazy<IAuthorRepository> _authorRepository;
        private readonly Lazy<IUserBookRepository> _userBookRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(repositoryContext));
            _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(repositoryContext));
            _userBookRepository = new Lazy<IUserBookRepository> (() => new UserBookRepository(repositoryContext));
            _commentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(repositoryContext));
        }

        public IBookRepository BookRepo => _bookRepository.Value;
        public IAuthorRepository AuthorRepo => _authorRepository.Value;
        public IUserBookRepository UserBookRepo => _userBookRepository.Value;
        public ICommentRepository CommentRepo => _commentRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
