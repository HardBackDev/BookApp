using BookAppServer.Dto.BooksDto;
using BookAppServer.Exceptions;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace BookAppServer.Contracts.ServicesContracts
{
    public interface IUserBookService
    {
        Task AddToFavoriteBooks(int bookId, string userName);
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetUserBooks(string userName, BookParameters parameters);
    }
}
