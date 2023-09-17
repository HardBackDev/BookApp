using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.RepositoriesContracts
{
    public interface IBookRepository
    {
        Task<PagedList<Book>> GetAll(BookParameters bookParameters);
        Task<Book> GetById(int id);
        Task<PagedList<Book>> GetBooksByAuthor(int authorId, BookParameters parameters);
        void CreateBook(Book book);
        void DeleteBook(Book book);
        void UpdateBook(Book book);
    }
}
