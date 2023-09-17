using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.RepositoriesContracts
{
    public interface IAuthorRepository
    {
        Task<PagedList<Author>> GetAll(AuthorParameters authorParameters);
        Task<Author> GetById(int id);
        Task<PagedList<Book>> GetBooksByAuthor(int authorId, BookParameters bookParameters);
        public void CreateAuthor(Author author);
        public void DeleteAuthor(Author author);
        Task UpdateAuthor(Author author);
    }
}
