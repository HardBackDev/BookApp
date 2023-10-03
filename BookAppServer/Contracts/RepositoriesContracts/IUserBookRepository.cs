using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.RepositoriesContracts
{
    public interface IUserBookRepository
    {
        void AddUserBook(int bookId, string userId);
        Task<PagedList<Book>> GetUserBooks(string userId, BookParameters parameters);
        Task<bool> CheckBookInFavorites(string userId, int bookId);
    }
}
