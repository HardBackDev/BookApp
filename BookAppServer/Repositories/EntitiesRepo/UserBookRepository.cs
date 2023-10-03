using BookAppServer.Contracts.RepositoriesContracts;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BookAppServer.Repositories.EntitiesRepo
{
    public class UserBookRepository : RepositoryBase<UserBook>, IUserBookRepository
    {
        public UserBookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void AddUserBook(int bookId, string userId)
        {
            var userBook = new UserBook() { BookId = bookId, UserId = userId};
            Create(userBook);
        }
        public async Task<PagedList<Book>> GetUserBooks(string userId, BookParameters parameters)
        {
            var books = await FindByCondition(u => u.UserId.Equals(userId))
            .Include(u => u.Book)
            .Select(u => u.Book)
            .Where(b => b.Title.Contains(parameters.TitleFilter ?? ""))
            .OrderBy(b => b.Title)
            .ToListAsync();

            return PagedList<Book>.ToPagedList(books, parameters.PageNumber, parameters.PageSize);

        }

        public async Task<bool> CheckBookInFavorites(string userId, int bookId)
        {
            return FindAll()
                .Any(ub => ub.UserId == userId && ub.BookId == bookId);
        }

    }
}
