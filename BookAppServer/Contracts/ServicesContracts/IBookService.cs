using BookAppServer.Dto.BooksDto;
using BookAppServer.Exceptions;
using BookAppServer.Models;
using BookAppServer.RequestFeatures;

namespace BookAppServer.Contracts.ServicesContracts
{
    public interface IBookService
    {
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooks(BookParameters bookParameters);
        Task<BookDto> GetBook(int id);
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetBooksByAuthor(int authorId, BookParameters bookParameters);
        Task<BookDto> CreateBook(BookForCreation bookForCreation);
        Task<IEnumerable<Book>> CreateBooks(IEnumerable<BookForCreation> booksForCreation);
        Task UpdateBook(int bookId, BookForUpdate bookForUpdate);
        Task DeleteBook(int id);
    }
}
