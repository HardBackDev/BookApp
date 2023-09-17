using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BookAppServer.Repositories.EntitiesRepo
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<PagedList<Author>> GetAll(AuthorParameters authorParameters)
        {
            var authors = await FindAll()
                .Where(a => a.Name.Contains(authorParameters.NameFilter ?? ""))
                .OrderBy(a => a.Name)
                .ToListAsync();

            return PagedList<Author>.ToPagedList(authors, authorParameters.PageNumber, authorParameters.PageSize);
        }
        public async Task<Author> GetById(int id) => 
            await FindByCondition(a => a.Id.Equals(id))
            .SingleOrDefaultAsync();

        public async Task<PagedList<Book>> GetBooksByAuthor(int authorId, BookParameters bookParameters)
        {
            var books = await _repositoryContext.Books
                .Where(b => b.Title.Contains(bookParameters.TitleFilter ?? "") 
                && b.AuthorId.Equals(authorId))
                .OrderBy(b => b.Title)
                .ToListAsync();
            
            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }
        public void CreateAuthor(Author author) => Create(author);
        public void DeleteAuthor(Author author) => Delete(author);
        public async Task UpdateAuthor(Author author) => await Update(author);
    }
}
