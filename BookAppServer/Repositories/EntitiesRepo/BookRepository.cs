using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BookAppServer.Repositories.EntitiesRepo
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<PagedList<Book>> GetAll(BookParameters bookParameters)
        {
            var query = FindByCondition(b => b.Title.Contains(bookParameters.TitleFilter ?? ""));

            if (bookParameters.IncludeAuthor)
                query = query.Include(b => b.Author);
            var books = await query
                .OrderBy(b => b.Title)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<Book> GetById(int id) => await FindByCondition(b => b.Id.Equals(id))
            .Include(b=>b.Author)
            .SingleOrDefaultAsync();

        public async Task<PagedList<Book>> GetBooksByAuthor(int authorId, BookParameters parameters)
        {
            var books = await FindByCondition(b => b.AuthorId.Equals(authorId) && b.Title.Contains(parameters.TitleFilter))
                .OrderByDescending(b => b.Title.Length)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books, parameters.PageNumber, parameters.PageSize);
        }
            
        public void CreateBook(Book book) => Create(book);
        public void DeleteBook(Book book) => Delete(book);
        public async void UpdateBook(Book book) => await Update(book);
    }
}
